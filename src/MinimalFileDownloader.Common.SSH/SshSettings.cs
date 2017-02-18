namespace MinimalFileDownloader.Common.SSH
{
    public class SshSettings
    {
        public string HostName { get; }
        public string UserName { get; }
        public string Password { get; }

        public string DownloadPath { get; }

        public SshSettings(string deviceHostName, string deviceUserName, string devicePassword, string downloadPath)
        {
            HostName = deviceHostName;
            UserName = deviceUserName;
            Password = devicePassword;

            DownloadPath = downloadPath;
        }
    }
}