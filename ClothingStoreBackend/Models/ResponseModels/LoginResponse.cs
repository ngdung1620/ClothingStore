#nullable enable
namespace ClothingStoreBackend.Models.ResponseModels
{
    public class LoginResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string? Token { get; set; }
    }
}