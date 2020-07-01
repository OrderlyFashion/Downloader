using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Downloader.ViewModels
{
	public class DownloadsViewModel : BaseViewModel
	{
		public ObservableCollection<DownloadViewModel> Downloads { get; set; } = new ObservableCollection<DownloadViewModel>();

		public Command LoadItemsCommand { get; set; }

		public DownloadsViewModel()
		{
			Title = "Downloads";
			LoadItemsCommand = new Command(() => {
				IsBusy = true;
				Downloads.Clear();
				RefreshDownloads();
				IsBusy = false;
			});
		}

		public void RefreshDownloads()
		{
			var downloads = DownloadManager.GetDownloads();

			foreach (var download in downloads)
			{
				var existingDownload = Downloads.FirstOrDefault(d => d.Id == download.Id);

				if (existingDownload != null)
				{
					existingDownload.UpdateValues(download);
				}
				else
				{
					Downloads.Add(new DownloadViewModel(download));
				}
			}

			var nonExistantDownloads = new List<DownloadViewModel>();

			foreach(var nonExistantDownload in Downloads.Where(d => !downloads.Any(existinDownload => existinDownload.Id == d.Id)))
			{
				nonExistantDownloads.Add(nonExistantDownload);
			}

			foreach (var toRemove in nonExistantDownloads)
			{
				Downloads.Remove(toRemove);
			}
		}
	}
}