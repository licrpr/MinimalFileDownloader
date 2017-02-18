namespace MinimalFileDownloader.Common.FTP
{
    public class FtpSettings
    {
        public string UserName { get; }
        public string Password { get; }
        public string Url { get; }

        public FtpSettings(string userName, string password, string url)
        {
            UserName = userName;
            Password = password;
            Url = url;
        }
    }
}