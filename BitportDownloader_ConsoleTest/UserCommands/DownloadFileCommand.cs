using BitportDownloader_SSH;
using System.Collections.Generic;
using System.Linq;

namespace BitportViewer.ConsoleTest
{
    internal class DownloadFileCommand : BaseUserCommand
    {
        public DownloadFileCommand(AppSettings appSettings, DownloadManager downloadManager, FtpService ftpService)
            : base(appSettings, downloadManager, ftpService)
        {
        }

        public override string Name
        {
            get
            {
                return "Download File or Folder";
            }
        }

        public override string Shortcut
        {
            get
            {
                return "download";
            }
        }

        public override void Run()
        {
            foreach (var folder in FtpService.ListFiles("/"))
            {
                ConsoleUtils.WriteOption(folder);
            }

            ConsoleUtils.DoCommandsWhileNotEscape("Path to download:", path =>
            {
                if (string.IsNullOrEmpty(path))
                {
                    ConsoleUtils.WriteLine("Path cannot be empty!");
                    return;
                }

                DownloadFile(path);
            });
        }

        private void DownloadFile(string path)
        {

            path = path.RemoveInitialSlah();

            var name = path.Split('/').LastOrDefault();

            var downloadInfo = new DownloadInfo($"ftp://{AppSettings.FtpUserName.FtpEscape()}:{AppSettings.FtpPassword.FtpEscape()}@ftp.bitport.io/{path.FtpEscape()}", name);

            DownloadManager.StartDownloadingFile(downloadInfo);
        }

        public override void RunSilent(IReadOnlyDictionary<string, string> parameters)
        {
            if (parameters == null || !parameters.ContainsKey("path"))
            {
                return;
            }

            string path = parameters["path"];

            DownloadFile(path);
        }
    }
}