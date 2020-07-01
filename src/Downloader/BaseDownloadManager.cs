using Downloader.Models.Download;
using Downloader.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Downloader
{
	public abstract class BaseDownloadManager : IDownloadManager
	{
		protected readonly List<DeviceDownload> _downloads;

		public BaseDownloadManager()
		{
			_downloads = new List<DeviceDownload>();
		}

		public async Task EnqueueDownload(IDownloadable downloadable)
		{
			await StartDeviceDownload(downloadable.DownloadUri, downloadable.Name);
		}

		protected abstract Task<bool> StartDeviceDownload(string downloadUri, string name);

		public abstract IEnumerable<IDownload> GetDownloads();
	}
}