using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models.OrderModels
{
    public class GetOrderOptionResponse
    {
        public List<OrderModel> ListOrder { get; set; }
        public int TotalPage { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
    }
}