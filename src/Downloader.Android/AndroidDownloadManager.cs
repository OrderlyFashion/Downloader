using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Downloader.Models.Download;
using DownloadStatus = Downloader.Models.Download.DownloadStatus;

[assembly: Xamarin.Forms.Dependency(typeof(Downloader.Droid.AndroidDownloadManager))]
namespace Downloader.Droid
{
	class AndroidDownloadManager : BaseDownloadManager
	{
		public override IEnumerable<IDownload> GetDownloads()
		{
			foreach (var download in _downloads)
			{
				var downloadState = GetDownloadState(download.Id);

				download.Status = downloadState?.Status ?? DownloadStatus.Failed;
				download.Reason = downloadState?.Reason ?? Reason.Error;
				download.TotalBytes = downloadState?.TotalBytes ?? -1;
				download.DownloadedBytes = downloadState?.DownloadedBytes ?? -1;
				download.DownloadLocation = downloadState?.DownloadLocation;
			}

			return _downloads;
		}

		protected override async Task<bool> StartDeviceDownload(string downloadUri, string name)
		{
			var downloadLongId = await BeginDownload(downloadUri, name);

			// TODO: Null check id
			if (downloadLongId == null)
			{
				return false;
			}

			var downloadId = downloadLongId.ToString();

			var downloadState = GetDownloadState(downloadId);
			var download = new DeviceDownload(downloadId, name, downloadState.Status, downloadState.Reason,
				downloadState.DownloadLocation, downloadState.TotalBytes, downloadState.DownloadedBytes);

			_downloads.Add(download);

			return true;
		}

		private async Task<long?> BeginDownload(string uri, string fileName)
		{
			return await EnqueueDownload(uri, System.IO.Path.GetFileName(fileName));
		}

		private async Task<long?> EnqueueDownload(string url, string fileName)
		{
			if (!await new PermissionRequester().RequestStorageWrite())
			{
				return null;
			}

			var source = Android.Net.Uri.Parse(url);
			var request = new DownloadManager.Request(source);
			request.AllowScanningByMediaScanner();
			request.SetAllowedOverRoaming(false);
			request.SetNotificationVisibility(DownloadVisibility.VisibleNotifyCompleted);
			request.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, source.LastPathSegment);
			request.SetTitle(fileName);
			request.SetDescription(fileName);
			var manager = (DownloadManager)Application.Context.GetSystemService(Context.DownloadService);

			return manager.Enqueue(request);
		}

		private DownloadState GetDownloadState(string id)
		{
			var longId = long.Parse(id);
			var manager = (DownloadManager)Application.Context.GetSystemService(Context.DownloadService);
			var query = new DownloadManager.Query();
			query.SetFilterById(longId);
			using var cursor = manager.InvokeQuery(query);

			if (cursor.MoveToFirst())
			{
				int status = cursor.GetInt(cursor.GetColumnIndex(DownloadManager.ColumnStatus));
				int reason = cursor.GetInt(cursor.GetColumnIndex(DownloadManager.ColumnReason));
				string downloadLocation = cursor.GetString(cursor.GetColumnIndex(DownloadManager.ColumnLocalUri));
				long totalSize = cursor.GetInt(cursor.GetColumnIndex(DownloadManager.ColumnTotalSizeBytes));
				long downloaded = cursor.GetInt(cursor.GetColumnIndex(DownloadManager.ColumnBytesDownloadedSoFar));

				cursor.Close();

				return new DownloadState(CreateDownloadStatus(status), CreateReason(reason),
					downloadLocation == null ? null : new Uri(downloadLocation), totalSize, downloaded);
			}

			return null;
		}

		private DownloadStatus CreateDownloadStatus(int id)
		{
			return id switch
			{
				1 => DownloadStatus.Pending,
				2 => DownloadStatus.Running,
				4 => DownloadStatus.Paused,
				8 => DownloadStatus.Successful,
				16 => DownloadStatus.Failed,
				_ => throw new ArgumentException($"No known DownloadStatus found for id {id}"),
			};
		}

		private Reason CreateReason(int id)
		{
			return id switch
			{
				0 => Reason.NoError,
				1 => Reason.PausedWaitingToRetry,
				2 => Reason.PausedWaitingForNetwork,
				3 => Reason.PausedQueuedForWifi,
				4 => Reason.PausedUnknown,
				int n when n >= 300 && n < 600 => Reason.ErrorUnhandledHttpCode, // Because of reasons...
				1000 => Reason.PausedUnknown,
				1001 => Reason.ErrorFileError,
				1002 => Reason.ErrorUnhandledHttpCode,
				1004 => Reason.ErrorHttpDataError,
				1005 => Reason.ErrorTooManyRedirects,
				1006 => Reason.ErrorInsufficientSpace,
				1007 => Reason.ErrorDeviceNotFound,
				1008 => Reason.ErrorCannotResume,
				1009 => Reason.ErrorFileAlreadyExists,
				_ => throw new ArgumentException($"No known Reason found for id {id}"),
			};
		}
	}
}