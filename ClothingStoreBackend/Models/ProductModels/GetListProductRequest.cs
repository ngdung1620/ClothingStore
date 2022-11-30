namespace ClothingStoreBackend.Models.ProductModels
{
    public class GetListProductRequest
    {
        public string Search { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}