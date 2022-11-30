using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStoreBackend.Models
{
    public class Cart
    {
        public Guid Id { get; set; }

        public List<ProductCart> ProductCarts { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}