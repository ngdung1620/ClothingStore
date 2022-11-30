using System.Collections.Generic;

namespace ClothingStoreBackend.Models.ResponseModels
{
    public class ListUserResponse
    {
        public List<UserResponse> Users { get; set; }
        public int TotalPage { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
    }
}