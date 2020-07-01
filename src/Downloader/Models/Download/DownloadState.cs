using System;
using System.Collections.Generic;
using System.Text;

namespace Downloader.Models.Download
{
	public class DownloadState
	{
		public DownloadState(DownloadStatus status, Reason reason, Uri downloadLocation, long totalBytes, long downloadedBytes)
		{
			Status = status;
			Reason = reason;
			DownloadLocation = downloadLocation;
			TotalBytes = totalBytes;
			DownloadedBytes = downloadedBytes;
		}

		public DownloadStatus Status { get; }

		public Reason Reason { get; }

		public Uri DownloadLocation { get; }

		public long TotalBytes { get; }

		public long DownloadedBytes { get; }
	}
}