using Microsoft.Extensions.Options;
using MinimalFileDownloader.App.WebApp.Models;
using MinimalFileDownloader.App.WebApp.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalFileDownloader.App.WebApp.Services
{
    public class DownloadsService : IDownloadsService
    {
        private readonly Common.SSH.IDownloadManager _downloadManager;
        private readonly IBrowserService _browserService;

        public DownloadsService(IOptions<SshSettings> settings, IBrowserService browserService)
        {
            _downloadManager = new Common.SSH.SshDownloadManager(settings.Value.ToDomainSettings());
            _browserService = browserService;
        }

        public Task<IReadOnlyCollection<DownloadResponse>> GetDownloadsAsync()
        {
            IReadOnlyCollection<DownloadResponse> downloads = _downloadManager.Downloads
                .Select(o => new DownloadResponse()
                {
                    Completion = o.Completion,
                    Path = o.Path,
                    Status = (DownloadStatus)o.Status
                })
                .ToArray();

            return Task.FromResult(downloads);
        }

        public Task StartDownloadingFilesAsync(StartDownloadRequest request)
        {
            //should first map from relative path to ftp url!!!
            var domainRequests = request.Paths.Select(o =>
             {
                 string url = _browserService.GetUrl(o.SourcePath);
                 string name = o.DestinationPath.RemoveInitialSlash();
                 return new Common.SSH.DownloadInfo(url, name);
             });
            _downloadManager.StartDownloadingFiles(domainRequests);
            return Task.CompletedTask;
        }

        public Task StopDownloadingFilesAsync(StopDownloadRequest request)
        {
            foreach (var file in request.Paths)
            {
                var downloadStates = _downloadManager
                    .Downloads
                    .Where(o => o.Path.EndsWith(file));
                foreach (var downloadState in downloadStates)
                {
                    _downloadManager.StopDownloadingFile(downloadState);
                }
            }
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _downloadManager.Dispose();
        }
    }
}