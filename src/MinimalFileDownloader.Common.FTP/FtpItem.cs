namespace MinimalFileDownloader.Common.FTP
{
    public class FtpItem : IFtpItem
    {
        public string Path { get; set; }
        public bool IsDirectory { get; set; }
        public bool IsFile { get; set; }
    }
}