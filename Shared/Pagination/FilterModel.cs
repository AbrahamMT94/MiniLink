using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLink.Shared.Pagination
{
    public class FilterModel
    {
        public string SearchString { get; set; }
        public string SortOrder { get; set; }

        public int PageIndex { get; set; } = 1;
    }
}
