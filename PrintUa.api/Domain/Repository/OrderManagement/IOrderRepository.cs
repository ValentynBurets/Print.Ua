using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Repositories.Base;

namespace Domain.Repository.OrderManagement
{
    public interface IOrderRepository : IEntityRepository<Order>
    {
        Task<IEnumerable<Order>> GetByOrderNumber(int orderNumber);
        Task<IEnumerable<Order>> GetByCustomerId(Guid customerId);
        Task<IEnumerable<Order>> GetByEmployeeId(Guid employeeId);
        Task<IEnumerable<Order>> GetByCreationDate(DateTime creationDate);
        Task<IEnumerable<Order>> GetByIsCanceled(bool isCanceled);
        Task<Order> GetByTTN(long ttn);
        Task<IEnumerable<Order>> GetByOrderState(OrderState orderState);
    }
}
