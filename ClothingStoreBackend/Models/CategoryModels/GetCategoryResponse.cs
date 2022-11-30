using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models.CategoryModels
{
    public class GetCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}