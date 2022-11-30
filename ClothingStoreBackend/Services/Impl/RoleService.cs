using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClothingStoreBackend.Models;
using ClothingStoreBackend.Models.RequestModels;
using ClothingStoreBackend.Models.ResponseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreBackend.Services.Impl
{
    public class RoleService: IRoleService
    {
        private readonly MasterDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleService(MasterDbContext context, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }


        public List<RoleResponse> GetAllRole()
        {
            var listRole = _context.Roles.Select(role => new RoleResponse
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();
            return listRole;
        }

        public async Task<RoleResponse> CreateRole(RoleRequest request)
        {
            var newRole = new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };
            await _roleManager.CreateAsync(newRole);
            await _context.SaveChangesAsync();
            return new RoleResponse
            {
                Id = newRole.Id,
                Name = newRole.Name
            };

        }

        public async Task<RespondAPI<string>> UpdateRoleAdminWithUIPermission(Guid Id, RequestUpdateRoleUI model)
        {
            ApplicationRole hasRole = await _roleManager.FindByIdAsync(Id.ToString());

            if (hasRole == null)
                return new RespondAPI<string>()
                    { Result = ResultRespond.NotFound, Code = "01", Message = "Không tìm thấy thông tin nhóm quyền" };
            
            IdentityResult roleResult = await _roleManager.UpdateAsync(hasRole);
            if (roleResult.Succeeded)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(hasRole);
                foreach (Claim claim in roleClaims)
                {
                    await _roleManager.RemoveClaimAsync(hasRole, claim);
                }
                
                foreach (string permission in model.ListPermission)
                {
                    await _roleManager.AddClaimAsync(hasRole, new Claim(ClaimTypes.Role, permission));
                }
                return new RespondAPI<string>()
                    { Result = ResultRespond.Succeeded, Message = "Cập nhật nhóm quyền thành công" };
            }
            return new RespondAPI<string>()
                { Result = ResultRespond.Failed, Code = "03", Message = "Không thể cập nhật nhóm quyền" };
        }

        public async Task<bool> DeleteRole(Guid id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(role => role.Id == id);
            if (role == null)
            {
                throw new Exception("Quyền này không tồn tại !");
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<GetRoleResponse> GetRole(Guid id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(role => role.Id == id);
            if (role == null)
            {
                throw new Exception("Quyền này không tồn tại !");
            }

            var claims = await _context.RoleClaims
                .Where(claim => claim.RoleId == role.Id)
                .Select(claim => claim.ClaimValue)
                .ToListAsync();
            return new GetRoleResponse()
            {
                RoleId = role.Id,
                Name = role.Name,
                Claims = claims
            };
        }
    }
}