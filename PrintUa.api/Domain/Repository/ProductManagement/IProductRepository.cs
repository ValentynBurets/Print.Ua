using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Repositories.Base;

namespace Domain.Repository.ProductManagement
{
    public interface IProductRepository : IEntityRepository<Product>
    {
        Task<IEnumerable<Product>> GetByOrderId(Guid orderId);
        Task<IEnumerable<byte[]>> GetPhotosByOrderId(Guid orderId);
        Task<IEnumerable<Product>> GetByServiceId(Guid serviceId);
    }
}
