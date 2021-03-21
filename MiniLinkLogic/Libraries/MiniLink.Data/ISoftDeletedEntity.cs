using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLinkLogic.Libraries.MiniLink.Data
{
    public partial interface ISoftDeletedEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        bool Deleted { get; set; }
    }
}
