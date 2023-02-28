using System;

namespace ClothingStoreBackend.Models.CategoryModels
{
    public class GetCategoryByOption
    {
        public Guid Id { get; set; }
        public int OptionSelect { get; set; }
    }
}