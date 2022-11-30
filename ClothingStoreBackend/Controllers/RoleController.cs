using System;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.RequestModels;
using ClothingStoreBackend.Services;
using ClothingStoreBackend.Settings;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController: ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("get-list-role")]
        public IActionResult GetListRole()
        {
            return Ok(_roleService.GetAllRole());
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] RoleRequest request)
        {
            return Ok(await _roleService.CreateRole(request));
        }
        [HttpPost("add-claim/{id}")]
        public async Task<IActionResult> AddClaim(Guid id, [FromBody] RequestUpdateRoleUI request)
        {
            return Ok(await _roleService.UpdateRoleAdminWithUIPermission(id, request));
        }

        [HttpGet("get-list-claim")]
        public IActionResult GetListClaim()
        {
            return Ok(SystemClaim.Claims);
        }

        [HttpGet("get-role/{id}")]
        public async Task<IActionResult> GetRole(Guid id)
        {
            return Ok(await _roleService.GetRole(id));
        }

        [HttpDelete("delete-role/{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            return Ok(await _roleService.DeleteRole(id));
        }
    }
}