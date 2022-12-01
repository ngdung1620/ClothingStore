using System;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.CartModels;
using ClothingStoreBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController: ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("get-cart/{id}")]
        public async Task<IActionResult> GetCart(Guid id)
        {
            return Ok( await _cartService.GetCart(id));
        }

        [HttpPost("add-product-cart")]
        public async Task<IActionResult> AddProductInCart(AddProductToCartRequest request)
        {
            return Ok(await _cartService.AddProductToCart(request));
        }

        [HttpPost("edit-product-cart")]
        public async Task<IActionResult> EditProductInCart(EditProductInCartRequest request)
        {
            return Ok(await _cartService.EditProductInCart(request));
        }

        [HttpDelete("delete-product-cart/{id}")]
        public async Task<IActionResult> DeleteProductInCart(Guid id)
        {
            return Ok(await _cartService.DeleteProductInCart(id));
        }
    }
}