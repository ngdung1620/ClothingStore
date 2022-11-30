using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClothingStoreBackend.Models;
using ClothingStoreBackend.Models.CategoryModels;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreBackend.Services.Impl
{
    public class CategoryService: ICategoryService
    {
        private readonly MasterDbContext _context;

        public CategoryService(MasterDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetListCategoryResponse>> GetListCategory()
        {
            var categories = await _context.Categories
                .Include(c => c.GroupCategory)
                .Select(category => new GetListCategoryResponse()
            {
                Id = category.Id,
                Name = category.Name,
                GroupCategoryId = category.GroupCategoryId
            }).ToListAsync();
            
            return categories;
        }

        public async Task<GetCategoryResponse> GetCategory(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                throw new Exception("Thể loại này không tồn tại ");
            }

            return new GetCategoryResponse()
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products
            };
        }

        public async Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest request)
        {
            var groupCategory = await _context
                    .GroupCategories.FirstOrDefaultAsync(gc => gc.Id == request.GroupCategoryId);
            if (groupCategory == null)
            {
                throw new Exception("Nhóm thể loại này không tồn tại !");
            }

            var newCategory = new Category()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                GroupCategory = groupCategory
            };
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return new CreateCategoryResponse()
            {
                Id = newCategory.Id,
                Name = newCategory.Name
            };
        }

        public async Task<EditCategoryResponse> EditCategory(EditCategoryRequest request)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (category == null)
            {
                throw new Exception("Thể lại này không tồn tại");
            }

            category.Name = request.Name;
            var groupCategory = await _context.GroupCategories
                    .FirstOrDefaultAsync(gc => gc.Id == request.GroupCategoryId);
            if (groupCategory == null)
            {
                throw new Exception("Nhóm thể loại này không tồn tại");
            }

            category.GroupCategory = groupCategory;
            await _context.SaveChangesAsync();
            return new EditCategoryResponse()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<bool> DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                throw new Exception("Thể lại này không tồn tại !");
            }

            _context.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}