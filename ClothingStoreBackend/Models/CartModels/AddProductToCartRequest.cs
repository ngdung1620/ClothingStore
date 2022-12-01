using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models.CartModels
{
    public class AddProductToCartRequest
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public int Size { get; set; }
    }
}