using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models.RequestModels
{
    public class EditUserRequest
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public int Gender { get; set; }
        public DateTime DoB { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public List<string> Roles { get; set; }
    }
}