using MinimalFileDownloader.App.WebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalFileDownloader.App.WebApp.Services
{
    public interface IDownloadsService
    {
        Task<IReadOnlyCollection<DownloadResponse>> GetDownloadsAsync();

        Task StartDownloadingFilesAsync(StartDownloadRequest request);

        Task StopDownloadingFilesAsync(StopDownloadRequest request);
    }
}