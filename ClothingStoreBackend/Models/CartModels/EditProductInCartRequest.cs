using System;

namespace ClothingStoreBackend.Models.CartModels
{
    public class EditProductInCartRequest
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}