using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLink.Shared.Messages
{
    public class LinkEntryVisitMessage
    {
        public Guid Id { get; set; }
        public string Ip { get; set; }
    }
}
