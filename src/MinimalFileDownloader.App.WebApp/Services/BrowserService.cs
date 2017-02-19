using Microsoft.Extensions.Options;
using MinimalFileDownloader.App.WebApp.Models;
using MinimalFileDownloader.App.WebApp.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalFileDownloader.App.WebApp.Services
{
    public class BrowserService : IBrowserService
    {
        private readonly Common.FTP.IFtpService _ftpService;
        private readonly FtpSettings _ftpSettings;

        public BrowserService(IOptions<FtpSettings> settings)
        {
            _ftpSettings = settings.Value;
            _ftpService = new Common.FTP.CoreFtpService(settings.Value.ToDomainSettings());
        }

        public async Task<IEnumerable<BrowseResponseItem>> GetItemsAsync(BrowseRequest request)
        {
            var items = await _ftpService
                .ListItemsAsync(request.Directory, request.Recursive, request.Type.HasFlag(BrowserRequestType.Files), request.Type.HasFlag(BrowserRequestType.Folders));

            return items
                .Select(o => new BrowseResponseItem() { Path = o.Path, Type = GetType(o) });
        }

        public string GetUrl(string sourcePath)
        {
            string url = $"ftp://{_ftpSettings.UserName.FtpEscape()}:{_ftpSettings.Password.FtpEscape()}@{_ftpSettings.Url}/{sourcePath.RemoveInitialSlash().FtpEscape()}";
            return url;
        }

        private static BrowseResponseItemType GetType(Common.FTP.IFtpItem item)
        {
            if (item.IsDirectory && !item.IsFile)
                return BrowseResponseItemType.Folder;
            else if (!item.IsDirectory && item.IsFile)
                return BrowseResponseItemType.File;
            else
                return BrowseResponseItemType.Unknown;
        }
        
        public void Dispose()
        {
            _ftpService.Dispose();
        }
    }
}