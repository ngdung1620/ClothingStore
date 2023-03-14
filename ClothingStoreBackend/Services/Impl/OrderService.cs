using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClothingStoreBackend.Models;
using ClothingStoreBackend.Models.OrderModels;
using ClothingStoreBackend.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClothingStoreBackend.Services.Impl
{
    public class OrderService: IOrderService
    {
            /*
             * 1: Chờ Xử lí
             * 2: Đang chuẩn bị hàng
             * 3: Đang Giao
             * 4: Hoàn thành
             * -1: Đã Huỷ
             */
        private readonly MasterDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public OrderService(MasterDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<CheckOutResponse> CheckOutHaveAccount(CheckOutHaveAccountRequest request)
        {
            var cart = await _context.Carts
                .Include(c => c.ProductCarts)
                .FirstOrDefaultAsync(c => c.Id == request.CartId);
            if (cart == null)
            {
                return new CheckOutResponse()
                {
                    Status = -2,
                    Message = "Không tìm thấy giỏ hàng"
                };
            }

            var listProductError = new List<Guid>();
            var isFall = false;
            cart.ProductCarts.ForEach(pc =>
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == pc.ProductId);
                if (product != null && pc.Quantity > product.Total)
                {
                    listProductError.Add(pc.ProductId);
                    isFall = true;
                }
            });
            if (isFall)
            {
                return new CheckOutResponse()
                {
                    Status = -1,
                    Message = "Sản lượng trong kho không đủ",
                    ProductError = listProductError
                };
            }

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            var order = new Order()
            {
                Id = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                Status = 1,
                TotalPrice = request.TotalPrice,
                ShippingFee = request.ShippingFee,
                CustomerName = request.CustomerName,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                ApplicationUser = user
            };
             _context.Orders.Add(order);
             cart.ProductCarts.ForEach(pc =>
             {
                 var product = _context.Products.FirstOrDefault(p => p.Id == pc.ProductId);
                 if (product != null)
                 {
                     var productOrder = new ProductOrder()
                     {
                         Id = Guid.NewGuid(),
                         Quantity = pc.Quantity,
                         Price = product.Price,
                         ProductId = product.Id,
                         OrderId = order.Id,
                         Size = pc.Size.ToString()
                     };
                     product.Total -= pc.Quantity;
                     product.Selling += pc.Quantity;
                     _context.ProductOrders.Add(productOrder);
                     _context.ProductCarts.Remove(pc);
                 }
             });
             await _context.SaveChangesAsync();
             return new CheckOutResponse()
             {
                 Status = 1,
                 Message = "Thêm vào giỏ hàng thành công",
                 OrderId = order.Id
             };
        }

        public async Task<CheckOutResponse> CheckOutDontHaveAccount(CheckOutDontHaveAccountRequest request)
        {
            /*
             * status = -1: không đủ số luong trong kho
             * status = -2: khong tim thay san pham
             * status = 1 : them thanh cong
             */
            var isFall = 0;
            var listProductError = new List<Guid>();
            var listProductNoFind = new List<Guid>();
            request.ItemOrders.ForEach(i =>
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == i.ProductId);
                if (product == null)
                {
                    isFall = -2;
                    listProductNoFind.Add(i.ProductId);
                }
                
                if (product != null && i.Quantity > product.Total && isFall != -2)
                {
                    isFall = -1;
                    listProductError.Add(product.Id);
                }
                
            });

            if (isFall == -2)
            {
                return new CheckOutResponse()
                {
                    Status = -2,
                    Message = "Không Tìm Thấy Sản Phẩm",
                    ProductError = listProductNoFind
                };
            }

            if (isFall == -1)
            {
                return new CheckOutResponse()
                {
                    Status = -1,
                    Message = "Vấn Đề Tồn Kho",
                    ProductError = listProductError
                };
            }
            var order = new Order()
            {
                Id = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                Status = 1,
                TotalPrice = request.TotalPrice,
                ShippingFee = request.ShippingFee,
                CustomerName = request.CustomerName,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
            };
            _context.Orders.Add(order);
            request.ItemOrders.ForEach(i =>
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == i.ProductId);
                if (product != null)
                {
                    var productOrder = new ProductOrder()
                    {
                        Id = Guid.NewGuid(),
                        Quantity = i.Quantity,
                        Price = product.Price,
                        ProductId = product.Id,
                        OrderId = order.Id,
                        Size = i.Size.ToString()
                    };
                    product.Total -= i.Quantity;
                    product.Selling += i.Quantity;
                    _context.ProductOrders.Add(productOrder);
                }
            });
            await _context.SaveChangesAsync();
            return new CheckOutResponse()
            {
                Status = 1,
                Message = "Thêm vào giỏ hàng thành công",
                OrderId = order.Id
            };
                    
        }

        public async Task<GetOrderResponse> GetOrder(Guid id)
        {
            var order = await _context.Orders
                .Include(o => o.ProductOrders)
                .FirstOrDefaultAsync(o => o.Id == id);
            var listProduct = new List<ProductOrderModel>();
            
            order.ProductOrders.ForEach(po =>
            {
                var p = _context.Products.FirstOrDefault(p => p.Id == po.ProductId);
                if (p != null)
                {
                    var product = new ProductOrderModel()
                    {
                        ProductId = po.ProductId,
                        ProductName = p.Name,
                        Quantity = po.Quantity,
                        Size = po.Size,
                        Price = po.Price,
                        Img = _configuration["Img:UrlImg"] + p.Img
                    };
                    listProduct.Add(product);
                }
            });

            return new GetOrderResponse()
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                Address = order.Address,
                Status = order.Status,
                ShippingFee = order.ShippingFee,
                CustomerName = order.CustomerName,
                TotalPrice = order.TotalPrice,
                PhoneNumber = order.PhoneNumber,
                ProductOrders = listProduct
            };
        }

        public async Task<List<GetOrderByUserIdResponse>> GetOrderByUserId(Guid id)
        {
            var listOrder = await _context.Orders
                .Include(o => o.ApplicationUser)
                .Where(o => o.ApplicationUser.Id == id)
                .Select(o => new GetOrderByUserIdResponse()
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                })
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            return listOrder;
        }

        public async Task<List<GetOrderWaitHandleResponse>> GetOrderWaitHandle()
        {
            var listOrder = await _context.Orders
                .Where(o => o.Status == 1)
                .Select(o => new GetOrderWaitHandleResponse()
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                    ShippingFee = o.ShippingFee,
                    CustomerName = o.CustomerName,
                    PhoneNumber = o.PhoneNumber,
                    Address = o.Address,
                })
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            return listOrder;

        }

        public GetOrderOptionResponse GetOrderOption(GetOrderOptionRequest request)
        {
            var allOrder = _context.Orders.AsQueryable();
            if(!String.IsNullOrEmpty(request.Search))
            {
                allOrder = allOrder
                    .Where(o => o.Id.ToString().ToLower().Contains(request.Search)
                                || o.CustomerName.ToLower().Contains(request.Search)
                                || o.PhoneNumber.Contains(request.Search));
            }

            if (request.Status != 0)
            {
                allOrder = allOrder.Where(o => o.Status == request.Status);
            }
            var result = PaginatedList<Order>.Create(allOrder, request.PageIndex, request.PageSize);
            var listOrder = result.Select(o => new OrderModel()
            {
                OrderId = o.Id,
                OrderDate = o.OrderDate,
                Status = o.Status,
                TotalPrice = o.TotalPrice,
                ShippingFee = o.ShippingFee,
                CustomerName = o.CustomerName,
                PhoneNumber = o.PhoneNumber,
                Address = o.Address,
            })
                .OrderByDescending(o => o.OrderDate)
                .ToList();
            return  new GetOrderOptionResponse()
            {
                ListOrder = listOrder,
                TotalPage = result.TotalPage,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize,
                TotalRecords = allOrder.Count(),
            };
        }

        public async Task<ChangeStatusResponse> ChangeStatus(ChangeStatusRequest request)
        {
            var order = await _context.Orders
                .Include(o => o.ProductOrders)
                .FirstOrDefaultAsync(o => o.Id == request.OrderId);
            if (request.Status == -1)
            {
                switch (order.Status)
                {
                    case 4:
                        order.ProductOrders.ForEach(po =>
                        {
                            var product = _context.Products.FirstOrDefault(p => p.Id == po.ProductId);
                            if (product != null)
                            {
                                product.Total += po.Quantity;
                                product.Sold -= po.Quantity;
                            }
                        });
                        break;
                    case -1: break;
                    default: 
                        order.ProductOrders.ForEach(po =>
                        {
                            var product = _context.Products.FirstOrDefault(p => p.Id == po.ProductId);
                            if (product != null)
                            {
                                product.Total += po.Quantity;
                                product.Selling -= po.Quantity;
                            }
                            
                        });
                        break;
                }
            }else 
            if (request.Status == 4)
            {
                switch (order.Status)
                {
                    case -1:
                        order.ProductOrders.ForEach(po =>
                        {
                            var product = _context.Products.FirstOrDefault(p => p.Id == po.ProductId);
                            if (product != null)
                            {
                                product.Total -= po.Quantity;
                                product.Sold += po.Quantity;
                            }
                        });
                        break;
                    case 4: break;
                    default: 
                        order.ProductOrders.ForEach(po =>
                        {
                            var product = _context.Products.FirstOrDefault(p => p.Id == po.ProductId);
                            if (product != null)
                            {
                                product.Sold += po.Quantity;
                                product.Selling -= po.Quantity;
                            }
                            
                        });
                        break;
                }
            }
            else
            {
                switch (order.Status)
                {
                    
                    case -1:
                        order.ProductOrders.ForEach(po =>
                        {
                            var product = _context.Products.FirstOrDefault(p => p.Id == po.ProductId);
                            if (product != null)
                            {
                                product.Total -= po.Quantity;
                                product.Selling += po.Quantity;
                            }
                            
                        });
                        break;
                    case 4: 
                        order.ProductOrders.ForEach(po =>
                        {
                            var product = _context.Products.FirstOrDefault(p => p.Id == po.ProductId);
                            if (product != null)
                            {
                                product.Sold -= po.Quantity;
                                product.Selling += po.Quantity;
                            }
                            
                        });
                        break;
                }
            }
            order.Status = request.Status;
            await _context.SaveChangesAsync();
            return new ChangeStatusResponse()
            {
                Message = "Thay đổi trạng thái thành công"
            };
            
        }

        public async Task<List<OrderModel>> Revenue(RevenueRequest request)
        {
            List<OrderModel> listOrder;
            if (request.OptionUpdate == 1)
            {
                listOrder = await _context.Orders
                .Where(o => o.Status == 4 && (o.OrderDate.Month >= request.StartTime.Month &&
                                              o.OrderDate.Month <= request.FinishTime.Month &&
                                              o.OrderDate.Year >= request.StartTime.Year &&
                                              o.OrderDate.Year <= request.FinishTime.Year))
                    .Select(o => new OrderModel()
                    {
                        OrderId = o.Id,
                        OrderDate = o.OrderDate,
                        Status = o.Status,
                        TotalPrice = o.TotalPrice,
                        ShippingFee = o.ShippingFee,
                        CustomerName = o.CustomerName,
                        PhoneNumber = o.PhoneNumber,
                        Address = o.Address,
                    })
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();
            }
            else
            {
                listOrder = await _context.Orders
                    .Where(o => o.Status == 4 && o.OrderDate >= request.StartTime && o.OrderDate < request.FinishTime.AddDays(1))
                    .Select(o => new OrderModel()
                    {
                        OrderId = o.Id,
                        OrderDate = o.OrderDate,
                        Status = o.Status,
                        TotalPrice = o.TotalPrice,
                        ShippingFee = o.ShippingFee,
                        CustomerName = o.CustomerName,
                        PhoneNumber = o.PhoneNumber,
                        Address = o.Address,
                    })
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();
            }
            return listOrder;
        }

        public DateTime GetMonthAndYear(DateTime time)
        {
            var formatTime = time.Year + "-"+ time.Month;
            return DateTime.Parse(formatTime);
        }
    }
}