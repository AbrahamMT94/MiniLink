using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniLink.Server.QueueService;
using MiniLink.Shared.Messages;
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
        private readonly IQueueService _queueService;

        public RedirectController(ILinkEntryService linkService, IQueueService queueService)
        {
            _linkService = linkService;
            _queueService = queueService;
        }


        // GET: RedirectController
        [Route("[controller]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length < 22)
            {
                return NotFound();
            }

            // base 64 does not alway produce url safe text therefore we need to decode it


            var entry = await _linkService.GetLinkEntryByBase64Id(id);

            if (entry is null)
                return NotFound();

            await _queueService.EnqueueVisit(entry.Id, Request.HttpContext.Connection.RemoteIpAddress.ToString());

            return Redirect(entry.URL);
        }

    }
}
