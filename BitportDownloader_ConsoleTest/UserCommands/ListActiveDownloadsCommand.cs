using BitportDownloader_SSH;
using System;
using System.Collections.Generic;

namespace BitportViewer.ConsoleTest
{
    internal class ListActiveDownloadsCommand : BaseUserCommand
    {
        public ListActiveDownloadsCommand(AppSettings appSettings, DownloadManager downloadManager, FtpService ftpService)
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

                DisplayActiveDownloads();
            }, cmd => { });
        }

        private void DisplayActiveDownloads()
        {
            IReadOnlyCollection<DownloadState> activeDownloads = DownloadManager.Downloads;

            ConsoleUtils.WriteLine("Active downloads:");

            foreach (var download in activeDownloads)
            {
                ConsoleUtils.WriteLine($"{download.Path} {download.Status} {download.Completion}");
            }
            ConsoleUtils.WriteLine("<Press ENTER to refresh>");
        }

        public override void RunSilent(IReadOnlyDictionary<string, string> parameters)
        {
            DisplayActiveDownloads();
        }
    }
}