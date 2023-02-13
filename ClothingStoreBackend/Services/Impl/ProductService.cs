using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClothingStoreBackend.Models;
using ClothingStoreBackend.Models.ProductModels;
using ClothingStoreBackend.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClothingStoreBackend.Services.Impl
{
    public class ProductService: IProductService
    {
        private readonly MasterDbContext _context;
        private readonly IConfiguration _configuration;

        public ProductService(MasterDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public GetListProductResponse GetListProduct(GetListProductRequest request)
        {
            var allProduct = _context.Products.AsQueryable();
            if (!String.IsNullOrEmpty(request.Search))
            {
                allProduct =  allProduct.Where(p => p.Name.ToLower().Contains(request.Search.ToLower()));
            }

            var result = PaginatedList<Product>.Create(allProduct, request.PageIndex, request.PageSize);
            var listProduct =  result.Select(p => new ProductResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Total = p.Total,
                Img = _configuration["Img:UrlImg"]+p.Img
            }).ToList();
            return new GetListProductResponse()
            {
                Products = listProduct,
                TotalPage = result.TotalPage,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize,
                TotalRecords = allProduct.Count()
            };
        }

        public async Task<List<ProductResponse>> GetAllProduct()
        {
            var listProduct = await _context.Products.Select(p => new ProductResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Total = p.Total,
                Img = _configuration["Img:UrlImg"] + p.Img
            }).ToListAsync();
            return listProduct;
        }

        public async Task<GetProductResponse> GetProduct(Guid id)
        {
            var product = await _context.Products
                .Include(p => p.ProductSizes)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                throw new Exception("Sản phẩm không tồn tại");
            }

            var listSizes = new List<ListSize>();
            product.ProductSizes.ForEach(ps =>
            {
                var size = _context.Sizes.FirstOrDefault(s => s.Id == ps.SizeId);
                if (size == null)
                {
                    throw new Exception("Kích thước không tồn tại");
                }

                var newSize = new ListSize()
                {
                    Id = size.Id,
                    Name = size.Name
                };
                listSizes.Add(newSize);
            });
            return new GetProductResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Total = product.Total,
                Img =  _configuration["Img:UrlImg"]+ product.Img,
                CategoryId = product.CategoryId,
                ListSizes = listSizes
            };
        }

        public async Task<CreateProductResponse> CreateProduct(CreateProductRequest request)
        {
            var fileName = request.ImgFile.FileName;
            try
            {
                string path = Path.Combine(@"C:\Hosting\Img", fileName);
                using (Stream stream = new FileStream(path,FileMode.Create))
                {
                    request.ImgFile.CopyTo(stream);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == request.CategoryId);
            if (category == null)
            {
                throw new Exception("Category không tồn tại");
            }
            
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                Total = request.Total,
                Img = fileName,
                PublicationDate = DateTime.Now,
                Sold = 0,
                Category = category
            };

            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            request.Sizes.ForEach(id =>
            {
                var size = _context.Sizes.FirstOrDefault(s => s.Id == id);
                if (size == null)
                {
                    throw new Exception("Kích thước này không tồn tại");
                }

                var productSize = new ProductSize()
                {
                    ProductId = product.Id,
                    SizeId = size.Id
                };
                _context.Add(productSize);
                _context.SaveChanges();
            });
            return new CreateProductResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Total = product.Total,
                Img = _configuration["Img:UrlImg"]+ product.Img,
                PublicationDate = product.PublicationDate
            };
        }

        public  async Task<EditProductResponse> EditProduct(EditProductRequest request)
        {
            var product = await _context.Products
                .Include(p => p.ProductSizes)
                .FirstOrDefaultAsync(p => p.Id == request.Id);
            if (product == null)
            {
                throw new Exception("Sản phẩm không tồn tại ");
            }

            if (request.ImgFile != null)
            {
                string path = Path.Combine(@"C:\Hosting\Img", product.Img);
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    System.IO.File.Delete(path);
                    file.Delete();
                }
                var fileName = request.ImgFile.FileName;
                try
                {
                    string path2 = Path.Combine(@"C:\Hosting\Img", fileName);
                    using (Stream stream = new FileStream(path2,FileMode.Create))
                    {
                        request.ImgFile.CopyTo(stream);
                    }

                    product.Img = fileName;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.Total = request.Total;
            product.Description = request.Description;
            product.CategoryId = request.CategoryId;
            product.ProductSizes.Clear();
            request.Sizes.ForEach(id =>
            {
                var size = _context.Sizes.FirstOrDefault(s => s.Id == id);
                if (size == null)
                {
                    throw new Exception("Kích thước này không tồn tại");
                }

                var productSize = new ProductSize()
                {
                    ProductId = product.Id,
                    SizeId = size.Id
                };
                if (product.ProductSizes != null)
                {
                    product.ProductSizes.Add(productSize);
                }
                else
                {
                    product.ProductSizes = new List<ProductSize>{productSize};
                }
            });
            
            await _context.SaveChangesAsync();
            
            return new EditProductResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Total = product.Total,
                Img = _configuration["Img:UrlImg"] + product.Img
            };
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                throw new Exception("Sản phẩm không tồn tại ");
            }
            _context.Remove(product);
            string path = Path.Combine(@"C:\Hosting\Img", product.Img);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                System.IO.File.Delete(path);
                file.Delete();
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductResponse>> GetSellingProduct()
        {
           var listProduct = await _context.Products
               .Select(p => new ProductResponse()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    Total = p.Total,
                    Img = _configuration["Img:UrlImg"] + p.Img,
                    PublicationDate = p.PublicationDate,
                    Sold = p.Sold
                })
               .OrderByDescending(p => p.Sold)
               .Take(10)
               .ToListAsync();
           return listProduct;
        }

        public async Task<List<ProductResponse>> GetNewProduct()
        {
            var listProduct = await _context.Products
                .Select(p => new ProductResponse()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    Total = p.Total,
                    Img = _configuration["Img:UrlImg"] + p.Img,
                    PublicationDate = p.PublicationDate,
                    Sold = p.Sold
                })
                .OrderByDescending(p => p.PublicationDate)
                .Take(10)
                .ToListAsync();
            return listProduct;
        }

        public async Task<List<ProductResponse>> SearchProduct(SearchProductRequest request)
        {
            var listProduct = await _context.Products
                .Where(p => p.Name.ToLower().Contains(request.Search.ToLower()))
                .Select(p => new ProductResponse()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    Total = p.Total,
                    Img =  _configuration["Img:UrlImg"] + p.Img,
                    Sold = p.Sold,
                    PublicationDate = p.PublicationDate,
                })
                .ToListAsync();
            return listProduct;
        }
    }
}