using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ClothingStoreBackend.Models.ProductModels
{
    public class GetProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Total { get; set; }
        public string Img { get; set; }
        public Guid CategoryId { get; set; }
        public List<ListSize> ListSizes { get; set; }
        
        
    }
}