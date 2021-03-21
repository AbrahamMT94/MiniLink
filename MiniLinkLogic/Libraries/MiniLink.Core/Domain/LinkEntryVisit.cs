using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLinkLogic.Libraries.MiniLink.Core.Domain
{
    public class LinkEntryVisit : BaseEntity
    {
        public DateTime TimeStamp { get; set; }

        public string VisitorIPAdress { get; set; }

        public Guid LinkEntryId { get; set; }

    }
}
