﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLinkLogic.Libraries.MiniLink.Core.Domain
{
    class LinkEntryVisit
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }

        public string VisitorIp { get; set; }

        public Guid LinkEntryId { get; set; }

    }
}
