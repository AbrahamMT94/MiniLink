using Microsoft.AspNetCore.Mvc;
using MiniLink.Server.Mappers;
using MiniLink.Server.Utilities;
using MiniLink.Shared;
using MiniLink.Shared.Pagination;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid? id)
        {
            var entry = await _linkService.GetLinkEntryById(id,true);

            if (entry is null)
                return NotFound();

            return Ok(LinkDTOPreparer.PrepareDTOWithCount(entry));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int pageIndex, string searchString, string sortOrder)
        {
            if (pageIndex < 1)
                pageIndex = 1;

            var entries = await _linkService.GetAllPaginated(pageIndex, searchString, sortOrder);

            if (entries is null)
                return NotFound();

           var dtoModel =  PaginatedModel<LinkWithCountDTO>.CreatePaginatedModel(entries.Select(m => LinkDTOPreparer.PrepareDTOWithCount(m)).ToList(),entries.PageIndex,entries.TotalPages, entries.TotalCount, entries.PageSize);

            return Ok(dtoModel);
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

            var url = Url.Action("Index","Redirect", new { id = GuidShortener.EncodeGuid(entry.Entry.Id) },"https", Request.Host.Value);
            
            return CreatedAtAction(nameof(Create), url );
        }
    }
}
