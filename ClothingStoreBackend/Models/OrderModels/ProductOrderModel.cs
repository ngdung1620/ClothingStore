using System;

namespace ClothingStoreBackend.Models.OrderModels
{
    public class ProductOrderModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public string Img { get; set; }
    }
}