using BitportDownloader_SSH;

namespace BitportViewer.ConsoleTest
{
    internal class AppSettings
    {
        //public string AppId { get; set; }
        //public string SecretKey { get; set; }
        //public int LocalhostCallbackPort { get; set; }

        public string DeviceHostName { get; set; }
        public string DeviceUserName { get; set; }
        public string DevicePassword { get; set; }
        public string DeviceDownloadsFolder { get; set; }

        public string FtpUserName { get; set; }
        public string FtpPassword { get; set; }
        public string FtpUrl { get; set; }

        public static implicit operator DeviceSetup(AppSettings appSettings)
        {
            return new DeviceSetup(appSettings.DeviceHostName, appSettings.DeviceUserName, appSettings.DevicePassword, appSettings.DeviceDownloadsFolder);
        }
    }
}