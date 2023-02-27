using System;

namespace ClothingStoreBackend.Models.OrderModels
{
    public class RevenueRequest
    {
        public int OptionUpdate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }
}