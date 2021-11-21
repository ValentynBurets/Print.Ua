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
    public class ProductRepository : EntityRepositoryClass<Product>, IProductRepository
    {
        public ProductRepository(DomainDbContext orderDbContext) : base(orderDbContext)
        {
        }

        public async Task<IEnumerable<Product>> GetByOrderId(Guid orderId)
        {
            return await _DbContext.Products.Where(e => e.OrderId == orderId).ToListAsync();
        }
        public async Task<IEnumerable<byte[]>> GetPhotosByOrderId(Guid orderId)
        {
            return await _DbContext.Products.Where(o => o.OrderId == orderId).Select(p => p.Picture).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetByServiceId(Guid serviceId)
        {
            return await _DbContext.Products.Where(e => e.ServiceId == serviceId).ToListAsync();
        }
    }
}
