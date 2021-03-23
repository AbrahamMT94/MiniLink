using MiniLink.Shared;
using MiniLinkLogic.Libraries.MiniLink.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniLink.Server.Mappers
{
   
        public static class LinkDTOPreparer
        {
            public static LinkDTO PrepareDTO(LinkEntry entry )
            {
                return new LinkDTO { Id = entry.Id, URL = entry.URL };
            }
        public static LinkWithCountDTO PrepareDTOWithCount(LinkEntry entry)
        {
            return new LinkWithCountDTO { Id = entry.Id, URL = entry.URL, VisitCount = entry.Visits };
        }
    }

   
}
