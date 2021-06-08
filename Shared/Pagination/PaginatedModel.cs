using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLink.Shared.Pagination
{
    [Serializable]
    public class PaginatedModel<T> : IPaginatedList<T>
    {

        public int PageIndex { get;  set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public IList<T> Items { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static PaginatedModel<T> CreatePaginatedModel(IList<T> list, int pageIndex, int totalPages,int count, int pageSize)
        {
            var newList = new PaginatedModel<T>() { PageIndex = pageIndex,TotalPages = totalPages,TotalCount = count,PageSize = pageSize };

            newList.Items = list;

            return newList;
            
        }
    }
}
