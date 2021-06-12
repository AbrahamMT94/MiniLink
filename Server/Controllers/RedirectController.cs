using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IBus _bus;

        public RedirectController(ILinkEntryService linkService, IBus bus)
        {
            _linkService = linkService;
            _bus = bus;
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

            try
            {
                Uri uri = new Uri("rabbitmq://rabbitmq:5672/visitQueue");

                var endPoint = await _bus.GetSendEndpoint(uri);
                var visit = new LinkEntryVisitMessage()
                {
                    Id = entry.Id,
                    Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                };

                await endPoint.Send(visit);
            }
            catch (Exception)
            {

               
            }

            return Redirect(entry.URL);
        }

    }
}
