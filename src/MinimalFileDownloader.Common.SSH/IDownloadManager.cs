using System;
using System.Collections.Generic;

namespace MinimalFileDownloader.Common.SSH
{
    public interface IDownloadManager : IDisposable
    {
        IReadOnlyCollection<IDownloadState> Downloads { get; }

        bool IsConnected { get; }

        void Reconect();

        void StartDownloadingFiles(IEnumerable<DownloadInfo> info);

        void StopDownloadingFile(IDownloadState state);
    }
}