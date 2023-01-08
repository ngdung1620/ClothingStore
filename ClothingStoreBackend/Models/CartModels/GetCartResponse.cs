using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models.CartModels
{
    public class GetCartResponse
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int Size { get; set; }
    }
}