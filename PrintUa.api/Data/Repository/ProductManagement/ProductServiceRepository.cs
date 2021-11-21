using Data.EF;
using Data.Repository.Base;
using Domain.Entity;
using Domain.Repository.ProductManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ProductServiceRepository : EntityRepositoryClass<ProductService>, IProductServiceRepository
    {
        public ProductServiceRepository(DomainDbContext orderDbContext) : base(orderDbContext)
        {
        }

        public async Task<IEnumerable<ProductService>> GetByCost(decimal cost)
        {
            return await _DbContext.Services.Where(e => e.Cost == cost).ToListAsync();
        }

        public async Task<IEnumerable<ProductService>> GetByFormatId(Guid formatId)
        {            
            return await _DbContext.Services.Where(e => e.FormatId == formatId).ToListAsync();
        }

        public async Task<IEnumerable<ProductService>> GetByMaterialId(Guid materialId)
        {
            return await _DbContext.Services.Where(e => e.MaterialId == materialId).ToListAsync();
        }
        public async Task<ProductService> GetByMaterialAndFormat(Guid materialId, Guid formatId)
        {
            return await _DbContext.Services.FirstOrDefaultAsync(e => e.MaterialId == materialId && e.FormatId == formatId);
        }
        public async Task<ProductService> GetByName(string name)
        {
            return await _DbContext.Services.FirstOrDefaultAsync(e => e.Name == name);
        }
        public async Task<IEnumerable<ProductService>> GetAllNotCanceled()
        {
            return await _DbContext.Set<ProductService>().Where(e => e.IsCanceled == false).ToListAsync();
        }
    }
}
