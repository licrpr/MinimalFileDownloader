namespace MinimalFileDownloader.App.WebApp.Models
{
    public class BrowseRequest
    {
        public string Directory { get; set; } = "/";
        public bool Recursive { get; set; } = true;
        public BrowserRequestType Type { get; set; } = BrowserRequestType.All;
    }
}