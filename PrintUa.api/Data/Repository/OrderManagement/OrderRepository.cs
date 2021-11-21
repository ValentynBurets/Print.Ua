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
    public class OrderRepository : EntityRepositoryClass<Order>, IOrderRepository
    {
        public OrderRepository(DomainDbContext orderDbContext) : base(orderDbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetByCreationDate(DateTime creationDate)
        {
            return await _DbContext.Orders.Where(b => b.CreationDate == creationDate).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByCustomerId(Guid customerId)
        {
            return await _DbContext.Orders.Where(b => b.CustomerId == customerId && b.IsCanceled == false).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByEmployeeId(Guid employeeId)
        {
            return await _DbContext.Orders.Where(b => b.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByIsCanceled(bool isCanceled)
        {
            return await _DbContext.Orders.Where(b => b.IsCanceled == isCanceled).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByOrderNumber(int orderNumber)
        {
            return await _DbContext.Orders.Where(b => b.OrderNumber == orderNumber).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByOrderState(OrderState orderState)
        {
            return await _DbContext.Orders.Where(b => b.State == orderState).ToListAsync();
        }

        public async Task<Order> GetByTTN(long TTN)
        {
            return await _DbContext.Orders.FirstAsync(b => b.TTN == TTN);
        }
    }
}
