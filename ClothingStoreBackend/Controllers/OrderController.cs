using System;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.OrderModels;
using ClothingStoreBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost("checkout-have-account")]
        public async Task<IActionResult> CheckOutHaveAccount(CheckOutHaveAccountRequest request)
        {
            return Ok(await _orderService.CheckOutHaveAccount(request));
        }

        [HttpPost("checkout-dont-have-account")]
        public async Task<IActionResult> CheckOutDontHaveAccount(CheckOutDontHaveAccountRequest request)
        {
            return Ok(await _orderService.CheckOutDontHaveAccount(request));
        }

        [HttpGet("get-order/{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            return Ok(await _orderService.GetOrder(id));
        }

        [HttpGet("get-lis-order-by-userId/{id}")]
        public async Task<IActionResult> GetListOrderByUserid(Guid id)
        {
            return Ok(await _orderService.GetOrderByUserId(id));
        }
    }
}