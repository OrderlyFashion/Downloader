using Downloader.Models.Download;

namespace Downloader.ViewModels
{
    public class DownloadViewModel : BaseViewModel
    {
        private IDownload _download;
        public DownloadViewModel(IDownload download)
        {
            UpdateValues(download);
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private DownloadStatus _status;
        public DownloadStatus DownloadStatus
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        private double _percentDownloaded;
        public double PercentDownloaded
        {
            get
            {
                return _percentDownloaded;
            }
            set { SetProperty(ref _percentDownloaded, value); }

        }

        private string _percentString;
        public string PercentString
        {
            get { return _percentString; }

            set { SetProperty(ref _percentString, value); }
        }

        public void UpdateValues(IDownload download)
        {
            _download = download;
            Id = _download.Id;
            Name = _download.Name;
            DownloadStatus = _download.Status;
            PercentDownloaded = _download.PercentDownloaded > 0 ? _download.PercentDownloaded : 0;
            PercentString = $"{(int)(PercentDownloaded * 100)} %";
        }
    }
}