using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models
{
    public class Size
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<ProductSize> ProductSizes { get; set; }
    }
}