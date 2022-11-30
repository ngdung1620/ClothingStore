using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ClothingStoreBackend.Models.ProductModels
{
    public class CreateProductRequest
    {
       
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Total { get; set; }
        public IFormFile ImgFile { get; set; }
        public List<Guid> Sizes { get; set; }
        public Guid CategoryId { get; set; }
    }
}