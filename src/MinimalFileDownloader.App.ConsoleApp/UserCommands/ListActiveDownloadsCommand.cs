using MinimalFileDownloader.Common.FTP;
using MinimalFileDownloader.Common.SSH;
using System;
using System.Collections.Generic;

namespace MinimalFileDownloader.App.ConsoleApp
{
    internal class ListActiveDownloadsCommand : BaseUserCommand
    {
        public ListActiveDownloadsCommand(AppSettings appSettings, IDownloadManager downloadManager, IFtpService ftpService)
            : base(appSettings, downloadManager, ftpService)
        {
        }

        public override string Name
        {
            get
            {
                return "ListActiveDownloads";
            }
        }

        public override string Shortcut
        {
            get
            {
                return "list";
            }
        }

        public override void Run()
        {
            ConsoleUtils.DoCommandsWhileNotEscape(() =>
            {
                Console.Clear();

                DisplayActiveDownloads(true);
            }, cmd => { });
        }

        private void DisplayActiveDownloads(bool canRefresh)
        {
            IReadOnlyCollection<IDownloadState> activeDownloads = DownloadManager.Downloads;

            ConsoleUtils.WriteLine("Active downloads:");

            foreach (var download in activeDownloads)
            {
                ConsoleUtils.WriteLine($"{download.Path} {download.Status} {download.Completion}");
            }
            if (canRefresh)
            {
                ConsoleUtils.WriteLine("<Press ENTER to refresh>");
            }
        }

        public override void RunSilent(IReadOnlyDictionary<string, string> parameters)
        {
            DisplayActiveDownloads(false);
        }
    }
}