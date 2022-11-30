using System;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.CategoryModels;
using ClothingStoreBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController: ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
            
        [HttpGet("get-list-category")]
        public async Task<IActionResult> GetListCategory()
        {
            return Ok(await _categoryService.GetListCategory());
        }

        [HttpGet("get-category/{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            return Ok(await _categoryService.GetCategory(id));
        }

        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            return Ok(await _categoryService.CreateCategory(request));
        }

        [HttpPost("edit-category")]
        public async Task<IActionResult> EditCategory(EditCategoryRequest request)
        {
            return Ok(await _categoryService.EditCategory(request));
        }

        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            return Ok(await _categoryService.DeleteCategory(id));
        }
    }
}