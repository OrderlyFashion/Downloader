using Downloader.Models.Download;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Networking.BackgroundTransfer;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(Downloader.UWP.UwpDownloadManager))]
namespace Downloader.UWP
{
	class UwpDownloadManager : BaseDownloadManager
	{
		private readonly List<DownloadOperation> _uwpDownloads;

		public UwpDownloadManager()
		{
			_uwpDownloads = new List<DownloadOperation>();
		}

		public override IEnumerable<IDownload> GetDownloads()
		{
			foreach (var download in _uwpDownloads)
			{
				yield return new DeviceDownload(download.Guid.ToString(), download.ResultFile.Name, CreateDownloadStatus(download.Progress.Status), Reason.NoError,
					new Uri(download.ResultFile.Path), (long)download.Progress.TotalBytesToReceive, (long)download.Progress.BytesReceived);
			}
		}

		protected override async Task<bool> StartDeviceDownload(string downloadUri, string name)
		{
			try
			{
				Uri source = new Uri(downloadUri);

				var destinationFile = await DownloadsFolder.CreateFileAsync(Path.GetFileName(source.LocalPath), CreationCollisionOption.GenerateUniqueName);
				var downloader = new BackgroundDownloader();
				var download = downloader.CreateDownload(source, destinationFile);
				_uwpDownloads.Add(download);
				var progressStuff = download.StartAsync();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Download Error", ex);
				return false;
			}

			return true;
		}

		private DownloadStatus CreateDownloadStatus(BackgroundTransferStatus uwpStatus)
		{
			return uwpStatus switch
			{
				BackgroundTransferStatus.Canceled => DownloadStatus.Failed,
				BackgroundTransferStatus.Completed => DownloadStatus.Successful,
				BackgroundTransferStatus.Error => DownloadStatus.Failed,
				BackgroundTransferStatus.Idle => DownloadStatus.Paused,
				BackgroundTransferStatus.PausedByApplication => DownloadStatus.Paused,
				BackgroundTransferStatus.PausedCostedNetwork => DownloadStatus.Paused,
				BackgroundTransferStatus.PausedNoNetwork => DownloadStatus.Paused,
				BackgroundTransferStatus.PausedRecoverableWebErrorStatus => DownloadStatus.Paused,
				BackgroundTransferStatus.PausedSystemPolicy => DownloadStatus.Paused,
				BackgroundTransferStatus.Running => DownloadStatus.Running,
				_ => throw new ArgumentException($"Got unexpected BackgroundTransferStatus: {uwpStatus}")
			};
		}
	}
}