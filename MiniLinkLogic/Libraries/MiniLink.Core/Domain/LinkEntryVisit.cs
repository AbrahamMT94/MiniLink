using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLinkLogic.Libraries.MiniLink.Core.Domain
{
    class LinkEntryVisit
    {
        public DateTime TimeStamp { get; set; }

        public string VisitorIPAdress { get; set; }

        public Guid LinkEntryId { get; set; }

    }
}
