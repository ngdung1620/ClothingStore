using System;

namespace ClothingStoreBackend.Models.OrderModels
{
    public class ChangeStatusRequest
    {
        public int Status { get; set; }
        public Guid OrderId { get; set; }
    }
}