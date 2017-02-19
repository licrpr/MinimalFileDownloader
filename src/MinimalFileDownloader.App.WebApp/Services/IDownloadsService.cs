using MinimalFileDownloader.App.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalFileDownloader.App.WebApp.Services
{
    public interface IDownloadsService : IDisposable
    {
        Task<IReadOnlyCollection<DownloadResponse>> GetDownloadsAsync();

        Task StartDownloadingFilesAsync(StartDownloadRequest request);

        Task StopDownloadingFilesAsync(StopDownloadRequest request);
    }
}