using System;

namespace ClothingStoreBackend.Models.CategoryModels
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
        public Guid GroupCategoryId { get; set; }
    }
}