using System;
using System.IO;

namespace MinimalFileDownloader.Common.SSH
{
    public class DownloadInfo
    {
        public string Url { get; }
        public string Name { get; }

        //download file with specific name
        public DownloadInfo(string url, string name = null)
        {
            Url = url;
            if (string.IsNullOrEmpty(name))
            {
                Uri uri = new Uri(url);
                Name = Path.GetFileName(uri.LocalPath);
            }
            else
            {
                Name = name;
            }
        }
    }
}