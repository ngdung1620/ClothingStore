using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClothingStoreBackend.Models;
using ClothingStoreBackend.Models.SizeModels;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreBackend.Services.Impl
{
    public class SizeService: ISizeService
    {
        private readonly MasterDbContext _context;

        public SizeService(MasterDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetListSizeResponse>> GetListSize()
        {
            var sizes = await _context.Sizes.Select(s => new GetListSizeResponse()
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
            return sizes;
        }

        public async Task<CreateSizeResponse> CreateSize(CreateSizeRequest request)
        {
            var size = new Size()
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };
            await _context.Sizes.AddAsync(size);
            await _context.SaveChangesAsync();
            return new CreateSizeResponse()
            {
                Id = size.Id,
                Name = size.Name
            };
        }

        public async Task<EditSizeResponse> EditSize(EditSizeRequest request)
        {
            var size = await _context.Sizes.FirstOrDefaultAsync(s => s.Id == request.Id);
            if (size == null)
            {
                throw new Exception("Size không tồn tại");
            }

            size.Name = request.Name;
            await _context.SaveChangesAsync();
            return new EditSizeResponse()
            {
                Id = size.Id,
                Name = size.Name
            };
        }

        public async Task<bool> DeleteSize(Guid id)
        {
            var size = await _context.Sizes.FirstOrDefaultAsync(s => s.Id == id);
            if (size == null)
            {
                throw new Exception("Size này không tồn tại");
            }
            _context.Remove(size);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}