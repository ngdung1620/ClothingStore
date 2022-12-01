using System;

namespace ClothingStoreBackend.Models.CartModels
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}