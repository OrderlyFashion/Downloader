using Downloader.Models.Download;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Downloader.Converters
{
    public class DownloadStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DownloadStatus downloadStatus))
                throw new ArgumentException("value not of type DownloadStatus");

            return downloadStatus switch
            {
                DownloadStatus.Running => Color.White,
                DownloadStatus.Paused => Color.FromHex("#FCFFA8"),
                DownloadStatus.Pending => Color.FromHex("#FCFFA8"),
                DownloadStatus.Successful => Color.FromHex("#80FF80"),
                DownloadStatus.Failed => Color.FromHex("#FF8080"),
                _ => throw new ArgumentException($"DownloadStatus had unexpected value: {downloadStatus}")
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("This converter does not support converting from Color to DownloadStatus");
        }
    }
}
