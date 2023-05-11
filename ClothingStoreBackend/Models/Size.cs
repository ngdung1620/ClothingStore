using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingStoreBackend.Models
{
    public class Size
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public List<ProductSize> ProductSizes { get; set; }
    }
}