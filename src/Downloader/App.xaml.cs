using Xamarin.Forms;
using Downloader.Services;
using Downloader.Views;

namespace Downloader
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();

			DependencyService.Register<IDownloadManager>();
			MainPage = new MainPage();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}