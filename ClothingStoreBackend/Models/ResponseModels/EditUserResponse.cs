using System;

namespace ClothingStoreBackend.Models.ResponseModels
{
    public class EditUserResponse
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public int Gender { get; set; }
        public DateTime DoB { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}