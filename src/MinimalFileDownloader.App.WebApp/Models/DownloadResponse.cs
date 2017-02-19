namespace MinimalFileDownloader.App.WebApp.Models
{
    public class DownloadResponse
    {
        public DownloadStatus Status { get; set; }

        public int? Completion { get; set; }

        public string Path { get; set; }
    }
}