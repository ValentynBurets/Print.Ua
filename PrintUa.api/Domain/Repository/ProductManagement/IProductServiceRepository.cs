using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Repositories.Base;

namespace Domain.Repository.ProductManagement
{
    public interface IProductServiceRepository : IEntityRepository<ProductService>
    {
        Task<ProductService> GetByName(string name);
        Task<IEnumerable<ProductService>> GetByCost(decimal cost);
        Task<IEnumerable<ProductService>> GetByMaterialId(Guid materialId);
        Task<IEnumerable<ProductService>> GetByFormatId(Guid formatId);
        Task<IEnumerable<ProductService>> GetAllNotCanceled();
        Task<ProductService> GetByMaterialAndFormat(Guid materialId, Guid formatId);
    }
}
