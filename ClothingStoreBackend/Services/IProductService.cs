using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.ProductModels;
using Microsoft.AspNetCore.Http;

namespace ClothingStoreBackend.Services
{
    public interface IProductService
    {
        GetListProductResponse GetListProduct(GetListProductRequest request);
        Task<List<ProductResponse>> GetAllProduct();
        Task<GetProductResponse> GetProduct(Guid id);
        Task<CreateProductResponse> CreateProduct(CreateProductRequest request);
        Task<EditProductResponse> EditProduct(EditProductRequest request);
        Task<bool> DeleteProduct(Guid id);
     
    }
}