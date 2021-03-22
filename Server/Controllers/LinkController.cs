using Microsoft.AspNetCore.Mvc;
using MiniLink.Shared;
using MiniLinkLogic.Libraries.MiniLink.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniLink.Server.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class LinkController : ControllerBase
    {
        private readonly ILinkEntryService _linkService;

        public LinkController(ILinkEntryService linkService)
        {
            _linkService = linkService;
        }

        public async Task<IActionResult> Get(Guid? id)
        {         
            var entry = await _linkService.GetLinkEntryById(id);

            if (entry is null)
                return NotFound();

            var count = await _linkService.GetVisitCount(id);           

           

            return Ok(LinkDTOPreparer.PrepareDTO(entry.Id, entry.URL, count));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLinkDTO input)
        {
            var entry = await _linkService.AddLinkEntry(input.URL, Request.HttpContext.Connection.RemoteIpAddress.ToString());

            if (!entry.Success)
            {
                foreach(var error in entry.Errors)
                {
                    ModelState.AddModelError(nameof(input.URL), error);
                }
                return BadRequest(ModelState);
            }

            var url = Url.Action("Index","Redirect", new { id = entry.Entry.Id },"https", Request.Host.Value);
            
            return CreatedAtAction(nameof(Create), url );
        }
    }
}
