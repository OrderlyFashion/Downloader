using System;
using System.Collections.Generic;
using System.Text;

namespace Downloader.Models.Download
{
	public class DeviceDownload : IDownload
	{
		public DeviceDownload(string id, string name, DownloadStatus status, Reason reason, Uri downloadLocation,
			long totalBytes, long downloadedBytes)
		{
			Id = id;
			Name = name;
			Status = status;
			Reason = reason;
			DownloadLocation = downloadLocation;
			TotalBytes = totalBytes;
			DownloadedBytes = downloadedBytes;
		}

		public string Id { get; }

		public string Name { get; }

		public DownloadStatus Status { get; set; }

		public Reason Reason { get; set; }

		public Uri DownloadLocation { get; set; }

		public long TotalBytes { get; set; }

		public long DownloadedBytes { get; set; }
	}
}