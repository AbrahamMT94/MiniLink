using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLinkLogic.Libraries.MiniLink.Core.Domain
{
    class LinkEntry
    {
        /// <summary>
        /// Id 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// URL to redirecto to
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Number of visits
        /// </summary>
        public long Visits { get; set; }

        /// <summary>
        /// Check whether the link is a premium link
        /// </summary>
        public bool PremiumLink { get; set; }   

    }
}
