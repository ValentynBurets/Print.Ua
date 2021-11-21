using Data.EF;
using Data.Repository.Base;
using Domain.Entity;
using Domain.Repository.ProductManagement;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ProductServiceMaterialRepository : EntityRepositoryClass<ServiceMaterial>, IProductServiceMaterialRepository
    {
        public ProductServiceMaterialRepository(DomainDbContext orderDbContext) : base(orderDbContext)
        {
        }

        public async Task<ServiceMaterial> GetByMaterialName(string materialName)
        {
            return await _DbContext.ServiceMaterials.FirstOrDefaultAsync(e => e.MaterialName == materialName);
        }
    }
}
