using BitportDownloader_SSH;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitportDownloader_DownloadManager
{
    [ImplementPropertyChanged]
    class DownloadInfoViewModel
    {
        public DownloadInfoViewModel()
        {
#if DEBUG

            Url = "https://fra2.bitport.io/246wccyvtud1pznnwr6dp8y32ujbkb4x";
            Name = "The.Exorcist.S01E06.HDTV.x264-FUM[ettv].mp4";
#endif
        }

        public string Url { get; set; }

        public bool ShowFtpParameters { get { return string.Equals(new Uri(Url).Scheme, "ftp", StringComparison.InvariantCultureIgnoreCase); } }
        public bool ShowName { get { return !ShowFtpParameters; } }
        
        public string Name { get; set; }
        public string FTP_UserName { get; }
        public string FTP_Password { get; }

        public DownloadInfo GetModel()
        {
            if (ShowFtpParameters)
                return new DownloadInfo(Url, FTP_UserName, FTP_Password);
            else
                return new DownloadInfo(Url, Name);
        }
    }
}
