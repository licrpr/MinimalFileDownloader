using BitportDownloader_SSH;
using System.Collections.Generic;

namespace BitportViewer.ConsoleTest
{
    internal abstract class BaseUserCommand : IUserCommand
    {
        public AppSettings AppSettings { get; }
        public DownloadManager DownloadManager { get; }
        public FtpService FtpService { get; }

        public BaseUserCommand(AppSettings appSettings, DownloadManager downloadManager, FtpService ftpService)
        {
            AppSettings = appSettings;
            DownloadManager = downloadManager;
            FtpService = ftpService;
        }

        public abstract string Name { get; }

        public abstract string Shortcut { get; }

        public abstract void Run();

        public abstract void RunSilent(IReadOnlyDictionary<string, string> parameters);
    }
}