﻿using System;
using System.Collections.Generic;

namespace ClothingStoreBackend.Models.RequestModels
{
    public class RegistrationRequest
    {
        public string FullName { get; set; }
        public int Gender { get; set; }
        public string DoB { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }
}