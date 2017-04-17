using MinimalFileDownloader.App.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalFileDownloader.App.WebApp.Services
{
    public interface IBrowserService : IDisposable
    {
        Task<IEnumerable<BrowseResponseItem>> GetItemsAsync(BrowseRequest request);

        string GetUrl(string sourcePath);
    }
}