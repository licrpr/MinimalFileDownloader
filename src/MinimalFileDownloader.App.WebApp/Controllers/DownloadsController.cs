using Microsoft.AspNetCore.Mvc;
using MinimalFileDownloader.App.WebApp.Models;
using MinimalFileDownloader.App.WebApp.Services;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalFileDownloader.App.WebApp.Controllers
{
    [Route("api/downloads")]
    public class DownloadsController : Controller
    {
        private readonly IDownloadsService _downloadsService;

        public DownloadsController(IDownloadsService downloadsService)
        {
            _downloadsService = downloadsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDownloadsAsync()
        {
            var downloads = await _downloadsService.GetDownloadsAsync();
            if (downloads == null || !downloads.Any())
            {
                return NoContent();
            }
            return Ok(downloads);
        }

        [HttpPost]
        public async Task<IActionResult> StartDownloadAsync([FromBody]StartDownloadRequest request)
        {
            await _downloadsService.StartDownloadingFilesAsync(request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> StopDownloadAsync([FromBody]StopDownloadRequest request)
        {
            await _downloadsService.StopDownloadingFilesAsync(request);
            return Ok();
        }
    }
}