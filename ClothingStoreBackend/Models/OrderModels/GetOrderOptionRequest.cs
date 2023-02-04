namespace ClothingStoreBackend.Models.OrderModels
{
    public class GetOrderOptionRequest
    {
        public string Search { get; set; }
        public int Status { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}