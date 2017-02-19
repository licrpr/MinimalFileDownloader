using Microsoft.AspNetCore.Mvc;
using MinimalFileDownloader.App.WebApp.Models;
using MinimalFileDownloader.App.WebApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalFileDownloader.App.WebApp.Controllers
{
    [Route("api/v0/downloads")]
    public class DownloadsController : Controller
    {
        private readonly IDownloadsService _downloadsService;

        public DownloadsController(IDownloadsService downloadsService)
        {
            _downloadsService = downloadsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DownloadResponse>), 200)]
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
        [ProducesResponseType(200)]
        public async Task<IActionResult> StartDownloadAsync([FromBody]StartDownloadRequest request)
        {
            await _downloadsService.StartDownloadingFilesAsync(request);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        public async Task<IActionResult> StopDownloadAsync([FromBody]StopDownloadRequest request)
        {
            await _downloadsService.StopDownloadingFilesAsync(request);
            return Ok();
        }
    }
}