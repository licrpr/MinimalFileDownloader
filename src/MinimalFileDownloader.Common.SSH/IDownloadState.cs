namespace MinimalFileDownloader.Common.SSH
{
    public interface IDownloadState
    {
        int? Completion { get; }
        string Path { get; }
        DownloadState.Statuses Status { get; }
    }
}