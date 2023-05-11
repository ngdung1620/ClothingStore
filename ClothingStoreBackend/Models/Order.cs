using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingStoreBackend.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public double TotalPrice { get; set; }
        public double ShippingFee { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        public ApplicationUser ApplicationUser { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
    }
}