using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingStoreBackend.Models
{
    public class GroupCategory
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public List<Category> Categories { get; set; }
    }
}