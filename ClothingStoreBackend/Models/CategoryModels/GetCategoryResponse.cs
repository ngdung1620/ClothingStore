using System;
using System.Collections.Generic;
using ClothingStoreBackend.Models.ProductModels;

namespace ClothingStoreBackend.Models.CategoryModels
{
    public class GetCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ProductResponse> Products { get; set; }
    }
}