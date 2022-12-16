using System;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.ProductModels;
using ClothingStoreBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromForm]CreateProductRequest request)
        {
            return Ok(await _productService.CreateProduct(request));
        }

        [HttpPost("get-list-product")]
        public IActionResult GetListProduct([FromBody] GetListProductRequest request)
        {
            return Ok( _productService.GetListProduct(request));
        }

        [HttpGet("get-all-product")]
        public async Task<IActionResult> GetAllProduct()
        {
            return Ok(await _productService.GetAllProduct());
        }
        [HttpGet("get-product/{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            return Ok(await _productService.GetProduct(id));
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            return Ok(await _productService.DeleteProduct(id));
        }

        [HttpPost("edit-product")]
        public async Task<IActionResult> EditProdut([FromForm]EditProductRequest request)
        {
            return Ok(await _productService.EditProduct(request));
        }
        
    }
}