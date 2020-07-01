using System;
using System.Collections.Generic;
using System.Text;

namespace Downloader.Models.Download
{
	public interface IDownload
	{
		public string Id { get; }

		public string Name { get; }

		public DownloadStatus Status { get; }

		public long TotalBytes { get; }

		public long DownloadedBytes { get; }
	}
}