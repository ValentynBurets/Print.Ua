using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Repositories.Base;

namespace Domain.Repository.OrderManagement
{
    public interface IOrderCommentRepository : IEntityRepository<OrderComment>
    {
        Task<IEnumerable<OrderComment>> GetByOrderId(Guid orderId);
        Task<IEnumerable<OrderComment>> GetByDate(DateTime date);
        Task<IEnumerable<OrderComment>> GetBySubject(string subject);
    }
}
