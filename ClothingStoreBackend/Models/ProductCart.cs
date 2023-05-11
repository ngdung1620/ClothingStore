using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingStoreBackend.Models
{
    public class ProductCart
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public int Size { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}