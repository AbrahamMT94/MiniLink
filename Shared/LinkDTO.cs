using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLink.Shared
{
    public class LinkDTO
    {
       

        public Guid Id { get; set; }
        public string URL { get; set; }
        public int VisitCount { get; set; }

    }

    public static class LinkDTOPreparer
    {
        public static LinkDTO PrepareDTO(Guid id, string url, int visitCount)
        {
            return new LinkDTO { Id = id, URL = url, VisitCount = visitCount };
        }
    }
   
        
}
