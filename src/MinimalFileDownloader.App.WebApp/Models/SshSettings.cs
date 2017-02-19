namespace MinimalFileDownloader.App.WebApp.Models
{
    public class SshSettings
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string DownloadPath { get; set; }

        public Common.SSH.SshSettings ToDomainSettings()
        {
            return new Common.SSH.SshSettings(HostName, UserName, Password, DownloadPath);
        }
    }
}