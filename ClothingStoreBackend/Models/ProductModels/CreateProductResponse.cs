using System;

namespace ClothingStoreBackend.Models.ProductModels
{
    public class CreateProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Total { get; set; }
        public string Img { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}