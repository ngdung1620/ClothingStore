using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingStoreBackend.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public double Price { get; set; }
        
        public string Description { get; set; }
        public int Total { get; set; }
        public string Img { get; set; }
        public DateTime PublicationDate { get; set; }
        public int Sold { get; set; }
        public int Selling { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public List<ProductSize> ProductSizes { get; set; }

        public List<ProductCart> ProductCarts { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
    }
}