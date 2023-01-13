using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.OrderModels;

namespace ClothingStoreBackend.Services
{
    public interface IOrderService
    {
        Task<CheckOutResponse> CheckOutHaveAccount(CheckOutHaveAccountRequest request);
        Task<CheckOutResponse> CheckOutDontHaveAccount(CheckOutDontHaveAccountRequest request);
        Task<GetOrderResponse> GetOrder(Guid id);
        Task<List<GetOrderByUserIdResponse>> GetOrderByUserId(Guid id);
    }
}