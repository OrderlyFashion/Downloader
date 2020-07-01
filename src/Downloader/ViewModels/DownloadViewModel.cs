using Downloader.Models.Download;

namespace Downloader.ViewModels
{
	public class DownloadViewModel : BaseViewModel
	{
		private IDownload _download;
		public DownloadViewModel(IDownload download)
		{
			UpdateValues(download);
		}

		private string _id;
		public string Id
		{
			get { return _id; }
			set { SetProperty(ref _id, value); }
		}

		private string _name;
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}

		private DownloadStatus _status;
		public DownloadStatus DownloadStatus
		{
			get { return _status; }
			set { SetProperty(ref _status, value); }
		}

		public double PercentDownloaded
		{
			get
			{
				return _download.DownloadedBytes == -1
					? 0f
					: (double)_download.DownloadedBytes / _download.TotalBytes;
			}
		}

		public string PercentString { get => $"{(int)(PercentDownloaded * 100)} %"; }

		public void UpdateValues(IDownload download)
		{
			_download = download;
			Id = _download.Id;
			Name = _download.Name;
			DownloadStatus = _download.Status;
		}
	}
}