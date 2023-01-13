using System.Collections.Generic;

namespace ClothingStoreBackend.Models.OrderModels
{
    public class CheckOutDontHaveAccountRequest
    {
        public List<ItemOrder> ItemOrders { get; set; }
        public double TotalPrice { get; set; }
        public double ShippingFee { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}