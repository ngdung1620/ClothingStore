using System;

namespace ClothingStoreBackend.Models.SizeModels
{
    public class EditSizeRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}