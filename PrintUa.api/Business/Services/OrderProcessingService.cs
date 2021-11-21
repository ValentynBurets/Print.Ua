using AutoMapper;
using Business.Interface.Models.OrdersInfoService;
using Business.Interface.Services;
using Data.Interface.UnitOfWork;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class OrderProcessingService : IOrderProcessingService
    {
        private readonly IOrderUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderProcessingService(IOrderUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Order> CreateOrder(Guid customerId, ICollection<Product> products, ICollection<OrderComment> comments)
        {
            if (products == null || products.Count == 0){
                throw new Exception("No products were given");
            }

            Order order = new Order();
            try
            {
                order.Customer = await _unitOfWork.CustomerRepository.GetByIdLink(customerId);
            }
            catch
            {
                throw new Exception("Customer was not found");
            }

            var timeNow = DateTime.Now;
            order.CreationDate = timeNow;
            order.State = OrderState.New;
            order.EmployeeId = null;
            order.IsCanceled = false;

            order.MyComments = comments;
            foreach (var comment in comments)
            {
                comment.Date = timeNow;
                comment.Subject = "wishes for a new order";
                await _unitOfWork.OrderCommentRepository.Add(comment);
            }

            try
            {
                int lastNumber = (await _unitOfWork.OrderRepository.GetAll()).Max(o => o.OrderNumber);
                order.OrderNumber = ++lastNumber;
            }
            catch
            {
                order.OrderNumber = 1;
            }

            await _unitOfWork.OrderRepository.Add(order);

            order.MyProducts = products;

            foreach (var product in products)
            {
                product.ContainingOrder = order;
                product.ServiceInProduct = await _unitOfWork.ProductServiceRepository.GetById(product.ServiceId); // handle this!
                product.ServiceId = Guid.Empty;
                await _unitOfWork.ProductRepository.Add(product);
            }

            await _unitOfWork.Save();

            return order;
        }

        public async Task<Order> CancelOrder(Guid orderId, Guid userId)
        {
            User user;

            user = await _unitOfWork.EmployeeRepository.GetByIdLinkOrDefault(userId);

            if (user == null)
            {
                user = await _unitOfWork.CustomerRepository.GetByIdLinkOrDefault(userId);
            }

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var order = await _unitOfWork.OrderRepository.GetById(orderId);
            order.IsCanceled = true;
            await _unitOfWork.Save();
            return order;
        }

        public Task<bool> SetOrderProducts(Guid orderId, ICollection<Product> products)
        {
            throw new NotImplementedException();
        }

        public async Task<T> StartWorkingWithOrder<T>(Guid id, Guid empId) where T : IOrderInfoModel
        {
            var order = await _unitOfWork.OrderRepository.GetById(id);

            if (order != null)
            {
                order.State = OrderState.InProgress;

                order.Employee = await _unitOfWork.EmployeeRepository.GetByIdLinkOrDefault(empId);

                if (order.Employee == null)
                {
                    throw new Exception("Employee was not found");
                }

                await _unitOfWork.Save();
                var orderInfo = _mapper.Map<Order, T>(order);

                return orderInfo;
            }
            else
            {
                throw new NullReferenceException("Orders enumerable was be null!");
            }
        }

        public async Task<Order> SetOrderTTN(Guid orderId, long ttn)
        {
            var order = await _unitOfWork.OrderRepository.GetById(orderId);

            order.TTN = ttn;
            order.State = OrderState.Ready;

            await _unitOfWork.Save();

            return order;
        }

        public async  Task<IEnumerable<Product>> GetProducts(Guid orderId)
        {
            var products = await _unitOfWork.ProductRepository.GetByOrderId(orderId);

            return products;
        }

        public async void UploadPhoto(Guid systemUserId, Guid productId, string newPhoto)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetById(productId);
                var order = await _unitOfWork.OrderRepository.GetById(product.OrderId);
                var user = await _unitOfWork.CustomerRepository.GetByIdLink(systemUserId);

                if (user.Id == order.CustomerId)
                {
                    product.Picture = Convert.FromBase64String(newPhoto);
                    await _unitOfWork.Save();
                }
            }
            catch
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
