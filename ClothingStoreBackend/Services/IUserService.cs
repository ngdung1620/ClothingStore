using System;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.RequestModels;
using ClothingStoreBackend.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreBackend.Services
{
    public interface IUserService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<CreateUserResponse> Registration(RegistrationRequest request);
        ListUserResponse GetListUser(ListUserRequest request);
        Task<string> ChangePassword(ChangePasswordRequest request);
        Task<UserResponse> GetUser(Guid id);
        Task<EditUserResponse> EditUser(EditUserRequest request);
        Task<bool> DeleteUser(Guid id);
       

    }
}