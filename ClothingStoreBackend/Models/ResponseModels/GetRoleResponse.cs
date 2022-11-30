using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models.ResponseModels
{
    public class GetRoleResponse
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public List<string> Claims { get; set; }
    }
}