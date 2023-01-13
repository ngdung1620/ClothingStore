using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public double TotalPrice { get; set; }
        public double ShippingFee { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        
        public ApplicationUser ApplicationUser { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
    }
}