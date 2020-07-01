using Downloader.Models.Download;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Downloader.ViewModels
{
	public class NodeViewModel : BaseViewModel
	{
		private Queue<IDownloadable> _downloadables;

		public NodeViewModel()
		{
			_downloadables = new Queue<IDownloadable>();
			_downloadables.Enqueue(new Downloadable("Test 1", "https://download4.dvdloc8.com/trailers/divxdigest/gravity_2k-trailer.zip"));
			_downloadables.Enqueue(new Downloadable("Test 2", "https://download4.dvdloc8.com/trailers/divxdigest/simpsons_movie_1080p_hddvd_trailer.zip"));
			_downloadables.Enqueue(new Downloadable("Test 3", "https://download4.dvdloc8.com/trailers/divxdigest/i_am_legend-1080p_blu-ray_trailer.zip"));
			_downloadables.Enqueue(new Downloadable("Test 4", "https://download4.dvdloc8.com/trailers/divxdigest/i_am_legend-1080p_trailer.zip"));
		}

		public async Task RequestDownload()
		{
			if (_downloadables.Any())
			{
				await DownloadManager.EnqueueDownload(_downloadables.Dequeue());
			}
		}
	}
}