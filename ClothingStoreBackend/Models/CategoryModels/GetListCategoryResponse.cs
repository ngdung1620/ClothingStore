﻿using System;

namespace ClothingStoreBackend.Models.CategoryModels
{
    public class GetListCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid GroupCategoryId { get; set; }
    }
}