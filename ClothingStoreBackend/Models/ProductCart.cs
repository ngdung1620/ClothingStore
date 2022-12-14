using System;

namespace ClothingStoreBackend.Models
{
    public class ProductCart
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public int Size { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}