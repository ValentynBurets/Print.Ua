using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Repositories.Base;

namespace Domain.Repository.ProductManagement
{
    public interface IProductServiceFormatRepository : IEntityRepository<ServiceFormat>
    {
        Task<ServiceFormat> GetByFormatName(string formatName);
    }
}
