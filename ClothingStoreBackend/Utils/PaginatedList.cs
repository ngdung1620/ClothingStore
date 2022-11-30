using System;
using System.Collections.Generic;
using System.Linq;

namespace ClothingStoreBackend.Utils
{
    public class PaginatedList<T>: List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex , int pageSize)
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            AddRange(items);
        }

        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex = 1, int pageSize = 5)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex-1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}