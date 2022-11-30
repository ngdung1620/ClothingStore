using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models.GroupCategoryModels
{
    public class GetGroupCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<CategoryResponse> Categories { get; set; }
    }
}