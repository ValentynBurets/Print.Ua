using Business.Interface.Models.OrdersInfoService;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface.Services
{
    public interface IOrderProcessingService
    {
        public Task<Order> CreateOrder(Guid customerId, ICollection<Product> products, ICollection<OrderComment> comments);

        public Task<bool> SetOrderProducts(Guid orderId, ICollection<Product> products);

        public Task<Order> SetOrderTTN(Guid orderId, long ttn);

        public Task<Order> CancelOrder(Guid orderId, Guid userId);

        public Task<T> StartWorkingWithOrder<T>(Guid orderId, Guid empId) where T : IOrderInfoModel;

        public Task<IEnumerable<Product>> GetProducts(Guid id);

        public void UploadPhoto(Guid systemUserId, Guid productId, string newPhoto);
    }
}
