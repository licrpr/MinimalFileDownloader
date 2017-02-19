using Microsoft.AspNetCore.Mvc;
using MinimalFileDownloader.App.WebApp.Models;
using MinimalFileDownloader.App.WebApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalFileDownloader.App.WebApp.Controllers
{
    [Route("api/v0/browser")]
    public class BrowserController : Controller
    {
        private readonly IBrowserService _browserService;

        public BrowserController(IBrowserService browserService)
        {
            _browserService = browserService;
        }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BrowseResponseItem>), 200)]
        public async Task<IActionResult> GetAsync([FromQuery]BrowseRequest request)
        {
            var items = await _browserService.GetItemsAsync(request);
            if (items == null || !items.Any())
            {
                return NoContent();
            }

            return Ok(items);
        }
    }
}