using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingStoreBackend.Models
{
    public class ProductOrder
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public double Price { get; set; }

        [Required]
        public string Size { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        
    }
}