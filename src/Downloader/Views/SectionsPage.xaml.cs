using Downloader.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Downloader.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SectionsPage : ContentPage
	{
		private readonly NodeViewModel _viewModel;

		public SectionsPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new NodeViewModel();
		}

		private void StartDownload(object sender, EventArgs e)
		{
			_viewModel.RequestDownload();
		}
	}
}