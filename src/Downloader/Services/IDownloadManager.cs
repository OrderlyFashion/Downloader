using Downloader.Models.Download;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Downloader.Services
{
	public interface IDownloadManager
	{
		Task EnqueueDownload(IDownloadable downloadable);

		IEnumerable<IDownload> GetDownloads();
	}
}