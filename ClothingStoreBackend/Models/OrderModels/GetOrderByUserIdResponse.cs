using System;

namespace ClothingStoreBackend.Models.OrderModels
{
    public class GetOrderByUserIdResponse
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public double TotalPrice { get; set; }
    }
}