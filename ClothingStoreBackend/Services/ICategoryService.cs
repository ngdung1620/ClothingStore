using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.CategoryModels;

namespace ClothingStoreBackend.Services
{
    public interface ICategoryService
    {
        Task<List<GetListCategoryResponse>> GetListCategory();
        Task<GetCategoryResponse> GetCategory(Guid id);
        Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest request);
        Task<EditCategoryResponse> EditCategory(EditCategoryRequest request);
        Task<bool> DeleteCategory(Guid id);
    }
}