using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClothingStoreBackend.Models;
using ClothingStoreBackend.Models.CartModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreBackend.Services.Impl
{
    public class CartService: ICartService
    {
        private readonly MasterDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartService(MasterDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<AddProductToCartResponse> AddProductToCart(AddProductToCartRequest request)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId);
            if (product == null)
            {
                return new AddProductToCartResponse()
                {
                    Status = -1,
                    Message = "Sản phẩm không tồn tại !"
                };
            }

            if (product.Total < request.Quantity)
            {
                return new AddProductToCartResponse()
                {
                    Status = -1,
                    Message = "Số lượng sản phẩm trong kho không đủ !"
                };
            }
            var productCart = await _context.ProductCarts
                .FirstOrDefaultAsync(pc => pc.CartId == request.CartId && pc.ProductId == request.ProductId);
            
            if (productCart != null && request.Size == productCart.Size)
            {
                productCart.Quantity = productCart.Quantity +  request.Quantity;
            }
            else
            {
                var newProductCart = new ProductCart()
                {
                    Id = Guid.NewGuid(),
                    CartId = request.CartId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    Size = request.Size
                };
                await _context.ProductCarts.AddAsync(newProductCart);
            }
            await _context.SaveChangesAsync();
            return new AddProductToCartResponse()
            {
                Status = 1,
                Message = "Thêm sản phẩm vào giỏ hàng thành công"
            };
        }

        public async Task<List<GetCartResponse>> GetCart(Guid id)
        {
            var productCarts = await _context.ProductCarts
                .Where(pc => pc.CartId == id)
                .ToListAsync();
            var listProductCart = new List<GetCartResponse>();
            productCarts.ForEach(pc =>
            {
                var product =  _context.Products.FirstOrDefault(p => p.Id == pc.ProductId);
                if (product == null)
                {
                    throw new Exception("Sản phẩm không tồn tại");
                }

                var productDetail = new ProductDetail()
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    Img = product.Img,
                    Size = pc.Size
                };
                var productCart = new GetCartResponse()
                {
                    Id = pc.Id,
                    Quantity = pc.Quantity,
                    ProductDetail = productDetail
                };
                listProductCart.Add(productCart);
            });
            return listProductCart;
        }

        public async Task<EditProductInCartResponse> EditProductInCart(EditProductInCartRequest request)
        {
            var productCart = await _context.ProductCarts
                .Include(pc => pc.Product)
                .FirstOrDefaultAsync(pc => pc.Id == request.Id);
            if (productCart == null)
            {
                return new EditProductInCartResponse()
                {
                    Status = -1,
                    Message = "Không tìm thấy sản phẩm này"
                };
            }

            if (productCart.Product.Total < request.Quantity)
            {
                return new EditProductInCartResponse()
                {
                    Status = -1,
                    Message = "Số lượng sản phẩm trong kho không đủ"
                };
            }

            productCart.Quantity = request.Quantity;
            await _context.SaveChangesAsync();
            return new EditProductInCartResponse()
            {
                Status = 1,
                Message = "Cập nhât thành công "
            };
        }

        public async Task<bool> DeleteProductInCart(Guid id)
        {
            var productCart = await _context.ProductCarts.FirstOrDefaultAsync(pc => pc.Id == id);
            if (productCart == null)
            {
                throw new Exception("Sản phẩm không tồn tại");
            }

            _context.ProductCarts.Remove(productCart);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}