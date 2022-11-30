using System;

namespace ClothingStoreBackend.Models.CategoryModels
{
    public class EditCategoryRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid GroupCategoryId { get; set; }
    }
}