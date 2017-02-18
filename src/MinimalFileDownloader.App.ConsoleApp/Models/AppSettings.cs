namespace MinimalFileDownloader.App.ConsoleApp
{
    internal class AppSettings
    {
        public SshSettings Device { get; set; }

        public FtpSettings Ftp { get; set; }
    }
}