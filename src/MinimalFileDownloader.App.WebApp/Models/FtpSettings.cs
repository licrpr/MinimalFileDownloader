namespace MinimalFileDownloader.App.WebApp.Models
{
    public class FtpSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }

        public Common.FTP.FtpSettings ToDomainSettings()
        {
            return new Common.FTP.FtpSettings(UserName, Password, Url);
        }
    }
}