using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.CartModels;

namespace ClothingStoreBackend.Services
{
    public interface ICartService
    {
        Task<AddProductToCartResponse> AddProductToCart(AddProductToCartRequest request);
        Task<List<GetCartResponse>> GetCart(Guid id);
        Task<EditProductInCartResponse> EditProductInCart(EditProductInCartRequest request);
        Task<bool> DeleteProductInCart(Guid id);
    }
}