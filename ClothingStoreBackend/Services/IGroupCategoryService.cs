using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.GroupCategoryModels;

namespace ClothingStoreBackend.Services
{
    public interface IGroupCategoryService
    {
        Task<CreateGroupCategoryResponse> CreateGroupCategory(CreateGroupCategoryRequest request);
        Task<List<GetGroupCategoryResponse>> GetListGroupCategory();
        Task<GetGroupCategoryResponse> GetGroupCategory(Guid id);
        Task<EditGroupCategoryResponse> EditGroupCategory(EditGroupCategoryRequest request);
        Task<bool> DeleteGroupCategory(Guid id);
    }
}