using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Interface.UnitOfWork;
using Domain.Entity;
using System.Linq;
using Business.Interface;
using Business.Interface.Models;
using AutoMapper;
using Business.Interface.Models.OrdersInfoService;
using Microsoft.AspNetCore.Identity;
using UserIdentity.Data;

namespace Business
{
    public class OrdersInfoService : IOrdersInfoService
    {
        private readonly IOrderUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersInfoService(IOrderUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : IOrderInfoModel
        {
            var orders = await _unitOfWork.OrderRepository.GetAll();

            var ordersInfo = _mapper.Map<IEnumerable<Order>, IEnumerable<T>>(orders);

            return ordersInfo;
        }

        public async Task<IEnumerable<T>> GetMy<T>(Guid userId, string role) where T : IOrderInfoModel
        {
            IEnumerable<Order> orders = null;

            if (role == "Customer")
            {
                userId = (await _unitOfWork.CustomerRepository.GetByIdLink(userId)).Id;
                orders = await _unitOfWork.OrderRepository.GetByCustomerId(userId);
            }
            else if (role == "Employee")
            {
                userId = (await _unitOfWork.EmployeeRepository.GetByIdLink(userId)).Id;
                orders = await _unitOfWork.OrderRepository.GetByEmployeeId(userId);
            }
            else
            {
                throw new ArgumentException("The specified role does not exist!");
            }

            if (orders != null)
            {
                var ordersInfo = _mapper.Map<IEnumerable<Order>, IEnumerable<T>>(orders);

                return ordersInfo;
            }
            else
            {
                throw new NullReferenceException("Orders enumerable was be null!");
            }
        }

        public async Task<T> GetById<T>(Guid userId, string role, Guid id) where T : IOrderInfoModel
        {
            Order order = null;
            
            if (role == "Customer")
            {
                var usr = await _unitOfWork.CustomerRepository.GetByIdLink(userId);
                order = await _unitOfWork.OrderRepository.GetById(id);
                var user = await _unitOfWork.CustomerRepository.GetByIdLink(userId);

                if (order.CustomerId != user.Id)
                {
                    return default(T);
                }
            }
            else if (role == "Employee")
            {
                order = await _unitOfWork.OrderRepository.GetById(id);
            }
            else
            {
                throw new ArgumentException("The specified role does not exist!");
            }

            if (order != null)
            {
                var orderInfo = _mapper.Map<Order, T>(order);

                return orderInfo;
            }
            else
            {
                throw new NullReferenceException("Orders enumerable was be null!");
            }
        }
    }
}
