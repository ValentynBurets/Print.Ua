using Data.EF;
using Data.Repository.Base;
using Domain.Entity;
using Domain.Repository.OrderManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class OrderCommentRepository : EntityRepositoryClass<OrderComment>, IOrderCommentRepository
    {
        public OrderCommentRepository(DomainDbContext orderDbContext) : base(orderDbContext)
        {
        }

        public async Task<IEnumerable<OrderComment>> GetByDate(DateTime date)
        {
            return await _DbContext.OrderComments.Where(e => e.Date == date).ToListAsync();
        }

        public async Task<IEnumerable<OrderComment>> GetByOrderId(Guid orderId)
        {
            return await _DbContext.OrderComments.Where(e => e.OrderId == orderId).ToListAsync();
        }

        public async Task<IEnumerable<OrderComment>> GetBySubject(string subject)
        {
            return await _DbContext.OrderComments.Where(e => e.Subject == subject).ToListAsync();
        }
    }
}
