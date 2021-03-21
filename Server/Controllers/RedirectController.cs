using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniLinkLogic.Libraries.MiniLink.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniLink.Server.Controllers
{
    public class RedirectController : Controller
    {
        private readonly ILinkEntryService _linkService;

        public RedirectController(ILinkEntryService linkService)
        {
            _linkService = linkService;
        }

        // GET: RedirectController
        public async Task<IActionResult> Index(Guid? id)
        {

            var entry = await _linkService.GetLinkEntryById(id);

            if (entry is null)
                return NotFound();

            await _linkService.AddVisit(entry, Request.HttpContext.Connection.RemoteIpAddress.ToString());

            return Redirect(entry.URL);
        }
       
    }
}
