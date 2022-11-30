using System;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.SizeModels;
using ClothingStoreBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SizeController: ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet("get-list-size")]
        public async Task<IActionResult> GetListSize()
        {
            return Ok(await _sizeService.GetListSize());
        }

        [HttpPost("create-size")]
        public async Task<IActionResult> CreateSize([FromBody]CreateSizeRequest request)
        {
            return Ok(await _sizeService.CreateSize(request));
        }

        [HttpDelete("delete-size/{id}")]
        public async Task<IActionResult> DeleteSize(Guid id)
        {
            return Ok(await _sizeService.DeleteSize(id));
        }

        [HttpPost("edit-size")]
        public async Task<IActionResult> EditSize([FromBody] EditSizeRequest request)
        {
            return Ok(await _sizeService.EditSize(request));
        }
    }
}