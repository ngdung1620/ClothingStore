using System;

namespace ClothingStoreBackend.Models.OrderModels
{
    public class OrderModel
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public double TotalPrice { get; set; }
        public double ShippingFee { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}