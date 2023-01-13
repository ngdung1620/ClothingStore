using System;

namespace ClothingStoreBackend.Models.OrderModels
{
    public class ItemOrder
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
    }
}