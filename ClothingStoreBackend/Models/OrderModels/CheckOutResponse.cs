using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models.OrderModels
{
    public class CheckOutResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public List<Guid> ProductError { get; set; }
        public Guid OrderId { get; set; }
    }
}