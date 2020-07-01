using Downloader.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Downloader.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DownloadsPage : ContentPage
	{
		private readonly DownloadsViewModel _viewModel;

		public DownloadsPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new DownloadsViewModel();

			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
			{
				Device.BeginInvokeOnMainThread(() => _viewModel.RefreshDownloads());
				return true;
			});
		}
	}
}