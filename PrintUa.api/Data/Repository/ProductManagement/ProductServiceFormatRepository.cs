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
    public class ProductServiceFormatRepository : EntityRepositoryClass<ServiceFormat>, IProductServiceFormatRepository
    {
        public ProductServiceFormatRepository(DomainDbContext orderDbContext) : base(orderDbContext)
        {
        }

        public async Task<ServiceFormat> GetByFormatName(string formatName)
        {
            return await _DbContext.ServiceFormats.FirstOrDefaultAsync(e => e.FormatName == formatName);
        }
    }
}
