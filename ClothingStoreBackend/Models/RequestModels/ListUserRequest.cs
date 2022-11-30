namespace ClothingStoreBackend.Models.RequestModels
{
    public class ListUserRequest
    {
        public string Search { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}