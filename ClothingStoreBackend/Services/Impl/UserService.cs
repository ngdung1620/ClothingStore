using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ClothingStoreBackend.Models;
using ClothingStoreBackend.Models.RequestModels;
using ClothingStoreBackend.Models.ResponseModels;
using ClothingStoreBackend.Settings;
using ClothingStoreBackend.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ClothingStoreBackend.Services.Impl
{
    public class UserService: IUserService
    {
        private readonly MasterDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserService(MasterDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Email);
            if (user == null)
            {
                return new LoginResponse()
                {
                    Status = -1,
                    Message = "Tài khoản hoặc mật khẩu không chính xác !"
                };
            }

            var loginResponse = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!loginResponse)
            {
                return new LoginResponse()
                {
                    Status = -1,
                    Message = "Mật khẩu không chính xác !"
                };
            }
            var token = await GenerateTokenJwt(user);
            return new LoginResponse
            {
                Status = 1,
                Message = "Đăng nhập thành công",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };
        }

        public async Task<CreateUserResponse> Registration(RegistrationRequest request)
        {
            var userOld = await _userManager.FindByNameAsync(request.Email);
            if (userOld != null)
            {
                return new CreateUserResponse()
                {
                    Status = -1,
                    Message = "Email đã được dùng !"
                };
            }
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = request.Email,
                FullName = request.FullName,
                Gender = request.Gender,
                DoB = DateTime.Parse(request.DoB),
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Email = request.Email
            };
            var newUser = await _userManager.CreateAsync(user, request.Password);
            if (!newUser.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in newUser.Errors)
                {
                    errors.Add(error.Description);
                }

                return new CreateUserResponse()
                {
                    Message = string.Join(", ", errors)
                };
            }
            await _userManager.AddToRolesAsync(user, request.Roles);
            var newCart = new Cart()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ApplicationUser = user
            };
            await _context.Carts.AddAsync(newCart);
            user.Cart = newCart;
            await _context.SaveChangesAsync();
            return new CreateUserResponse()
            {
                Status = 1,
                Message = "Đăng Kí thành công"
            };
        }

        public ListUserResponse GetListUser(ListUserRequest request)
        {
            var allUser =  _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(request.Search))
            {
                allUser = allUser.Where(user => user.FullName.ToLower().Contains(request.Search.ToLower()));
            }

            var result = PaginatedList<ApplicationUser>.Create(allUser, request.PageIndex, request.PageSize);
            var listUser = result.Select(user => new UserResponse()
            {
                UserId = user.Id,
                FullName = user.FullName,
                Gender = user.Gender,
                DoB = user.DoB,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Email = user.Email,
            }).ToList();
            return new ListUserResponse()
            {
                Users = listUser,
                TotalPage = result.TotalPage,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize,
                TotalRecords = allUser.Count()
            };

        }

        public async Task<string> ChangePassword(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                return string.Join(", ", errors);
            }

            return "Đổi mật khẩu thành công";
        }

        public async Task<UserResponse> GetUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new Exception("Tài khoản không tồn tại");
            }

            var roles = await _userManager.GetRolesAsync(user);
            return new UserResponse()
            {
                UserId = user.Id,
                FullName = user.FullName,
                Gender = user.Gender,
                DoB = user.DoB,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Email = user.Email,
                Roles = roles
            };
        }

        public async Task<EditUserResponse> EditUser(EditUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                throw new Exception("Tài khoản không tồn tại");
            }

            user.FullName = request.FullName;
            user.Address = request.Address;
            user.Gender = request.Gender;
            user.DoB = request.DoB;
            user.PhoneNumber = request.PhoneNumber;
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }
            await _userManager.AddToRolesAsync(user,request.Roles);
            await _context.SaveChangesAsync();
           return new EditUserResponse()
           {
               UserId = user.Id,
               FullName = user.FullName,
               Gender = user.Gender,
               DoB = user.DoB,
               PhoneNumber = user.PhoneNumber,
               Address = user.Address,
               Email = user.Email,
           };
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new Exception("Tài khoản không tồn tại !");
            }

            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == id);
            if (cart == null)
            {
                throw new Exception("Giỏ hàng không tồn tại");
            }
            _context.Users.Remove(user);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<JwtSecurityToken> GenerateTokenJwt(ApplicationUser user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (string role in userRoles)
            {
                var roleData = await _roleManager.FindByNameAsync(role);
                if (roleData != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(roleData);
                    foreach (Claim claim in roleClaims)
                    {
                        authClaims.Add(claim);
                    }
                }
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DefaultApplication.SecretKey));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(24),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
            return token;
        }
    }
}