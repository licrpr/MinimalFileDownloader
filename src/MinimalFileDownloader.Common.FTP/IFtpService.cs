using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalFileDownloader.Common.FTP
{
    public interface IFtpService : IDisposable
    {
        Task<IReadOnlyCollection<string>> ListFilesAsync(string path);

        Task<IReadOnlyCollection<string>> ListFoldersAsync(string path);
    }
}