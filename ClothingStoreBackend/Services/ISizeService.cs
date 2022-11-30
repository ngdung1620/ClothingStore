using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClothingStoreBackend.Models.SizeModels;

namespace ClothingStoreBackend.Services
{
    public interface ISizeService
    {
        Task<List<GetListSizeResponse>> GetListSize();
        Task<CreateSizeResponse> CreateSize(CreateSizeRequest request);
        Task<EditSizeResponse> EditSize(EditSizeRequest request);
        Task<bool> DeleteSize(Guid id);
    }
}