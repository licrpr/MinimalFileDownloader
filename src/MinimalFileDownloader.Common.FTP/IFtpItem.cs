namespace MinimalFileDownloader.Common.FTP
{
    public interface IFtpItem
    {
        string Path { get; }
        bool IsDirectory { get; }
        bool IsFile { get; }
    }
}