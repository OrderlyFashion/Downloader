namespace Downloader.Models.Download
{
	class Downloadable : IDownloadable
	{
		public Downloadable(string name, string downloadUri)
		{
			Name = name;
			DownloadUri = downloadUri;
		}

		public string Name { get; }

		public string DownloadUri { get; }
	}
}