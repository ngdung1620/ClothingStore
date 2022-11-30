using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models.ProductModels
{
    public class GetListProductResponse
    {
        public List<ProductResponse> Products { get; set; } 
        public int TotalPage { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
    }
}