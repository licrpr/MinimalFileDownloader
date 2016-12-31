using BitportDownloader_SSH;
using System.Collections.Generic;
using System.Linq;

namespace BitportViewer.ConsoleTest
{
    internal class SyncFolderCommand : BaseUserCommand
    {
        public SyncFolderCommand(AppSettings appSettings, DownloadManager downloadManager, FtpService ftpService)
            : base(appSettings, downloadManager, ftpService)
        {
        }

        public override string Name
        {
            get
            {
                return "Sync File or Folder";
            }
        }

        public override string Shortcut
        {
            get
            {
                return "sync";
            }
        }

        public override void Run()
        {
            foreach (var folder in FtpService.ListFolders("/"))
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

                DownloadFilesFromFolder(path);
            });
        }

        private void DownloadFilesFromFolder(string path)
        {
            path = path.AddInitialSlash();

            var files = FtpService.ListFiles(path);

            var downloadInfos = files
                .Select(file => new DownloadInfo($"ftp://{AppSettings.FtpUserName.FtpEscape()}:{AppSettings.FtpPassword.FtpEscape()}@ftp.bitport.io/{file.RemoveInitialSlah().FtpEscape()}", file.RemoveInitialSlah()))
                .ToArray();

            DownloadManager.StartDownloadingFiles(downloadInfos);
        }

        public override void RunSilent(IReadOnlyDictionary<string, string> parameters)
        {
            if (parameters == null || !parameters.ContainsKey("path"))
            {
                return;
            }

            string path = parameters["path"];

            DownloadFilesFromFolder(path);
        }
    }
}