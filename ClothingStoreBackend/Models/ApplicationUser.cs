using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ClothingStoreBackend.Models
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public int Gender { get; set; }
        public DateTime DoB { get; set; }
        public string Address { get; set; }
        public Cart Cart { get; set; }
    }
}