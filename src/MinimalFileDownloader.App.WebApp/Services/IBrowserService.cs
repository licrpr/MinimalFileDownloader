using MinimalFileDownloader.App.WebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalFileDownloader.App.WebApp.Services
{
    public interface IBrowserService
    {
        Task<IEnumerable<BrowseResponseItem>> GetItemsAsync(BrowseRequest request);

        string GetUrl(string sourcePath);
    }
}