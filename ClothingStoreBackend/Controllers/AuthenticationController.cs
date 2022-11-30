using System;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.RequestModels;
using ClothingStoreBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController: ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _userService.Login(request);
            return Ok(response);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest request)
        {
            var response = await _userService.Registration(request);
            return Ok(response);
        }

        [HttpPost("get-list-user")]
        public IActionResult GetListUser([FromBody] ListUserRequest request)
        {
            return Ok(_userService.GetListUser(request));
        }

        [HttpGet("get-user/{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            return Ok(await _userService.GetUser(id));
        }

        [HttpPost("edit-user")]
        public async Task<IActionResult> EditUser([FromBody] EditUserRequest request)
        {
            return Ok(await _userService.EditUser(request));
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            return Ok(await _userService.ChangePassword(request));
        }
        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            return Ok(await _userService.DeleteUser(id));
        }

    }
}