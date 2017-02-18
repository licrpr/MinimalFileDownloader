using MinimalFileDownloader.Common.FTP;
using MinimalFileDownloader.Common.SSH;
using System.Collections.Generic;

namespace MinimalFileDownloader.App.ConsoleApp
{
    internal abstract class BaseUserCommand : IUserCommand
    {
        public AppSettings AppSettings { get; }
        public IDownloadManager DownloadManager { get; }
        public IFtpService FtpService { get; }

        public BaseUserCommand(AppSettings appSettings, IDownloadManager downloadManager, IFtpService ftpService)
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