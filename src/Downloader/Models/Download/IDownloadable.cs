using System;
using System.Collections.Generic;
using System.Text;

namespace Downloader.Models.Download
{
	public interface IDownloadable
	{
		string Name { get; }

		string DownloadUri { get; }
	}
}