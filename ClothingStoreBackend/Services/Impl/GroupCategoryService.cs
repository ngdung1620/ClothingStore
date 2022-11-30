using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClothingStoreBackend.Models;
using ClothingStoreBackend.Models.GroupCategoryModels;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreBackend.Services.Impl
{
    public class GroupCategoryService: IGroupCategoryService
    {
        private readonly MasterDbContext _context;

        public GroupCategoryService(MasterDbContext context)
        {
            _context = context;
        }

        public async Task<CreateGroupCategoryResponse> CreateGroupCategory(CreateGroupCategoryRequest request)
        {
            var newGroupCategory = new GroupCategory()
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };
             await _context.GroupCategories.AddAsync(newGroupCategory);
             await _context.SaveChangesAsync();
             return new CreateGroupCategoryResponse()
             {
                 Id = newGroupCategory.Id,
                 Name = newGroupCategory.Name
             };
        }

        public async Task<List<GetGroupCategoryResponse>> GetListGroupCategory()
        {
            var groupCategories = await _context.GroupCategories
                .Include(gc => gc.Categories)
                .Select(gc => gc).ToListAsync();
            var listGroupCategory = new List<GetGroupCategoryResponse>();
            groupCategories.ForEach(gc =>
            {
                List<CategoryResponse> categories;
                categories = gc.Categories.Select(c => new CategoryResponse()
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();
                var groupCategory = new GetGroupCategoryResponse()
                {
                    Id = gc.Id,
                    Name = gc.Name,
                    Categories = categories
                };
                
                listGroupCategory.Add(groupCategory);
            });
            return listGroupCategory;


        }

        public async Task<GetGroupCategoryResponse> GetGroupCategory(Guid id)
        {
            var groupCategory = await _context.GroupCategories.FirstOrDefaultAsync(gc => gc.Id == id);
            if (groupCategory == null)
            {
                throw new Exception("Nhóm thể loại không tồn tại");
            }

            return new GetGroupCategoryResponse()
            {
                Id = groupCategory.Id,
                Name = groupCategory.Name
            };
        }

        public async Task<EditGroupCategoryResponse> EditGroupCategory(EditGroupCategoryRequest request)
        {
            var groupCategory = await _context.GroupCategories
                .FirstOrDefaultAsync(gc => gc.Id == request.Id);
            if (groupCategory == null)
            {
                throw new Exception("Nhóm thể loại không tồn tại");
            }
            groupCategory.Name = request.Name;
            await _context.SaveChangesAsync();
            return new EditGroupCategoryResponse()
            {
                Id = groupCategory.Id,
                Name = groupCategory.Name
            };
        }

        public async Task<bool> DeleteGroupCategory(Guid id)
        {
            var groupCategory = await _context.GroupCategories.FirstOrDefaultAsync(gc => gc.Id == id);
            if (groupCategory == null)
            {
                throw new Exception("Nhóm thể loại không tồn tại");
            }

            _context.Remove(groupCategory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}