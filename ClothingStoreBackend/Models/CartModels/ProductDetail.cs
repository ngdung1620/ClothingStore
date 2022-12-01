using System;
using System.Collections.Generic;
using ClothingStoreBackend.Models.ProductModels;

namespace ClothingStoreBackend.Models.CartModels
{
    public class ProductDetail
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int Size { get; set; }
    }
}