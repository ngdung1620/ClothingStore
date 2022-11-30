using System;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.GroupCategoryModels;
using ClothingStoreBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupCategoryController: ControllerBase
    {
        private readonly IGroupCategoryService _groupCategoryService;

        public GroupCategoryController(IGroupCategoryService groupCategoryService)
        {
            _groupCategoryService = groupCategoryService;
        }

        [HttpGet("get-list-group-category")]
        public async Task<IActionResult> GetListGroupCategory()
        {
            return Ok( await _groupCategoryService.GetListGroupCategory());
        }

        [HttpGet("get-group-category/{id}")]
        public async Task<IActionResult> GetGroupCategory(Guid id)
        {
            return Ok(await _groupCategoryService.GetGroupCategory(id));
        }

        [HttpPost("create-group-category")]
        public async Task<IActionResult> CreateGroupCategory([FromBody] CreateGroupCategoryRequest request)
        {
            return Ok(await _groupCategoryService.CreateGroupCategory(request));
        }

        [HttpPost("edit-group-category")]
        public async Task<IActionResult> EditGroupCategory([FromBody] EditGroupCategoryRequest request)
        {
            return Ok(await _groupCategoryService.EditGroupCategory(request));
        }

        [HttpDelete("delete-group-category/{id}")]
        public async Task<IActionResult> DeleteGroupCategory(Guid id)
        {
            return Ok(await _groupCategoryService.DeleteGroupCategory(id));
        }
    }
}