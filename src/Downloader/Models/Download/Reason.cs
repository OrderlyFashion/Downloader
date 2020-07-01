using System;
using System.Collections.Generic;
using System.Text;

namespace Downloader.Models.Download
{
	public enum Reason
	{
		NoError, ErrorCannotResume, ErrorDeviceNotFound, ErrorFileAlreadyExists, ErrorFileError, ErrorHttpDataError,
		ErrorInsufficientSpace, ErrorTooManyRedirects, ErrorUnhandledHttpCode, ErrorUnknown, Error, PausedQueuedForWifi, PausedUnknown,
		PausedWaitingForNetwork, PausedWaitingToRetry
	}
}