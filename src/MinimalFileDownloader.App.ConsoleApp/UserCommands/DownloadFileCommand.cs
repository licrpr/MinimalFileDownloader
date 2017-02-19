using MinimalFileDownloader.Common.FTP;
using MinimalFileDownloader.Common.SSH;
using System.Collections.Generic;
using System.Linq;

namespace MinimalFileDownloader.App.ConsoleApp
{
    internal class DownloadFileCommand : BaseUserCommand
    {
        public DownloadFileCommand(AppSettings appSettings, IDownloadManager downloadManager, IFtpService ftpService)
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
            foreach (var folder in FtpService.ListItemsAsync("/", true, true, false).GetAwaiter().GetResult())
            {
                ConsoleUtils.WriteOption(folder.Path);
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
            path = path.RemoveInitialSlash();

            var name = path.Split('/').LastOrDefault();

            var downloadInfo = new DownloadInfo($"ftp://{AppSettings.Ftp.UserName.FtpEscape()}:{AppSettings.Ftp.Password.FtpEscape()}@ftp.bitport.io/{path.FtpEscape()}", name);

            DownloadManager.StartDownloadingFiles(new[] { downloadInfo });
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