using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Essentials;

namespace Downloader.Droid
{
	[Activity(Label = "PermissionRequester")]
	class PermissionRequester : Activity
	{
		public async Task<bool> RequestStorageWrite()
		{
			if (await Permissions.CheckStatusAsync<Permissions.StorageWrite>() != PermissionStatus.Granted)
			{
				if (await Permissions.RequestAsync<Permissions.StorageWrite>() != PermissionStatus.Granted)
				{
					return false;
				}
			}

			return true;
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			Platform.Init(this, savedInstanceState);
		}
	}
}