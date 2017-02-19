using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalFileDownloader.Common.FTP
{
    public interface IFtpService : IDisposable
    {
        Task<IReadOnlyCollection<IFtpItem>> ListItemsAsync(string directory, bool recursive, bool getFiles, bool getFolders);
    }
}