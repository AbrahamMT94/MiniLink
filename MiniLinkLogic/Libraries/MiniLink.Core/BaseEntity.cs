using MiniLink.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLinkLogic.Libraries.MiniLink.Core
{
    public class BaseEntity
    {
        public BaseEntity()
        {
                Id = SequentialGuidGenerator.Create(SequentialGuidType.SequentialAtEnd);
        }

        /// <summary>
        /// Key
        /// </summary>
        public Guid Id { get; set; }
    }
}
