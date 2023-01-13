using System;

namespace ClothingStoreBackend.Models
{
    public class ProductOrder
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public string Size { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        
    }
}