using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models.CartModels
{
    public class GetCartResponse
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public ProductDetail ProductDetail { get; set; }
    }
}