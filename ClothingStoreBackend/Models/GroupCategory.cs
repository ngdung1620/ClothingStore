using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models
{
    public class GroupCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public List<Category> Categories { get; set; }
    }
}