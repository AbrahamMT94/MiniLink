using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniLinkLogic.Libraries.MiniLink.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MiniLink.Server.Controllers
{
    [ApiController]

    public class RedirectController : Controller
    {
        private readonly ILinkEntryService _linkService;

        public RedirectController(ILinkEntryService linkService)
        {
            _linkService = linkService;
        }

        // GET: RedirectController
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length < 22)
            {
                return NotFound();
            }

            var entry = await _linkService.GetLinkEntryByBase64Id(id);

            if (entry is null)
                return NotFound();

            await _linkService.AddVisit(entry.Id, Request.HttpContext.Connection.RemoteIpAddress.ToString());

            return Redirect(entry.URL);
        }

    }
}
