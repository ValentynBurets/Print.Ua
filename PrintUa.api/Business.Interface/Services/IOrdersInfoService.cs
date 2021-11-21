using Business.Interface.Models.OrdersInfoService;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IOrdersInfoService
    {
        public Task<IEnumerable<T>> GetAll<T>() where T : IOrderInfoModel;
        public Task<IEnumerable<T>> GetMy<T>(Guid userId, string role) where T : IOrderInfoModel;
        public Task<T> GetById<T>(Guid userId, string role, Guid id) where T : IOrderInfoModel;
    }
}
