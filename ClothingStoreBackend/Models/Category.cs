using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingStoreBackend.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public List<Product> Products { get; set; }

        public Guid GroupCategoryId { get; set; }
        public GroupCategory GroupCategory { get; set; }
    }
}