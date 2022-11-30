using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Total { get; set; }
        public string Img { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public List<ProductSize> ProductSizes { get; set; }

        public List<ProductCart> ProductCarts { get; set; }
    }
}