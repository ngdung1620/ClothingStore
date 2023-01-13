using System;

namespace ClothingStoreBackend.Models.OrderModels
{
    public class CheckOutHaveAccountRequest
    {
        public Guid CartId { get; set; }
        public double TotalPrice { get; set; }
        public double ShippingFee { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
    }
}