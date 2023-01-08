using System;
using System.Collections.Generic;
using ClothingStoreBackend.Models.CartModels;

namespace ClothingStoreBackend.Models.ResponseModels
{
    public class UserResponse
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public int Gender { get; set; }
        public DateTime DoB { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Guid CartId { get; set; }
        public int TotalItemInCart { get; set; }
        public IList<string> Roles { get; set; }
    }
}