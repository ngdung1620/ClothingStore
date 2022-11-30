using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.RequestModels;
using ClothingStoreBackend.Models.ResponseModels;

namespace ClothingStoreBackend.Services
{
    public interface IRoleService
    {
        List<RoleResponse> GetAllRole();
        Task<RoleResponse> CreateRole(RoleRequest request);
        Task<RespondAPI<string>> UpdateRoleAdminWithUIPermission(Guid Id, RequestUpdateRoleUI model);
        Task<bool> DeleteRole(Guid id);
        Task<GetRoleResponse> GetRole(Guid id);
    }
}