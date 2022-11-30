using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public List<Product> Products { get; set; }

        public Guid GroupCategoryId { get; set; }
        public GroupCategory GroupCategory { get; set; }
    }
}