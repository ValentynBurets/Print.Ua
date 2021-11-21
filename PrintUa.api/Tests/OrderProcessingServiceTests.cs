using Api.Configurations;
using Api.Models.OrderController.DetailOrderInfoModels;
using AutoMapper;
using Business.Services;
using Data.Interface.UnitOfWork;
using Domain.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BusinessTests
{
    public class OrderProcessingServiceTests
    {
        private static IMapper _mapper;

        public OrderProcessingServiceTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapperInitializer());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }


        static List<Customer> customers = new List<Customer>
        {
            new Customer(Guid.NewGuid()) { Id = Guid.NewGuid(), Name = "CustomerName1", Surname = "CustomerSurname1" },
            new Customer(Guid.NewGuid()) { Id = Guid.NewGuid(), Name = "CustomerName2", Surname = "CustomerSurname2" },
            new Customer(Guid.NewGuid()) { Id = Guid.NewGuid(), Name = "CustomerName3", Surname = "CustomerSurname3" }
        };
        static List<Employee> employees = new List<Employee>
        {
            new Employee(Guid.NewGuid()) { Id = Guid.NewGuid(), Name = "EmployeeName1", Surname = "EmployeeSurname1" },
            new Employee(Guid.NewGuid()) { Id = Guid.NewGuid(), Name = "EmployeeName2", Surname = "EmployeeSurname2" },
            new Employee(Guid.NewGuid()) { Id = Guid.NewGuid(), Name = "EmployeeName3", Surname = "EmployeeSurname3" },
        };
        static List<ServiceFormat> serviceFormats = new List<ServiceFormat>
        {
            new ServiceFormat { Id = Guid.NewGuid(), FormatName = "Format1" },
            new ServiceFormat { Id = Guid.NewGuid(), FormatName = "Format2" },
            new ServiceFormat { Id = Guid.NewGuid(), FormatName = "Format3" }
        };
        static List<ServiceMaterial> serviceMaterials = new List<ServiceMaterial>
        {
            new ServiceMaterial { Id = Guid.NewGuid(), MaterialName = "Material1" },
            new ServiceMaterial { Id = Guid.NewGuid(), MaterialName = "Material2" },
            new ServiceMaterial { Id = Guid.NewGuid(), MaterialName = "Material3" }
        };
        static List<ProductService> services = new List<ProductService>
        {
            new ProductService { Id = Guid.NewGuid(), Name = "Service1", Cost = 100, Format = serviceFormats[0], Material = serviceMaterials[0] },
            new ProductService { Id = Guid.NewGuid(), Name = "Service2", Cost = 200, Format = serviceFormats[1], Material = serviceMaterials[1] },
            new ProductService { Id = Guid.NewGuid(), Name = "Service3", Cost = 300, Format = serviceFormats[2], Material = serviceMaterials[2] }
        };
        static List<Product> products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Picture = new byte[] { 1, 2, 3 }, Amount = 1, ServiceInProduct = services[0] },
            new Product { Id = Guid.NewGuid(), Picture = new byte[] { 4, 5, 6 }, Amount = 2, ServiceInProduct = services[1] },
            new Product { Id = Guid.NewGuid(), Picture = new byte[] { 7, 8, 9 }, Amount = 3, ServiceInProduct = services[2] }
        };
        static List<Order> orders = new List<Order>
        {
            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customers[0].Id,
                Customer = customers[0],
                EmployeeId = employees[0].Id,
                Employee = employees[0],
                OrderNumber = 111,
                State = OrderState.Ready,
                TTN = 0,
                CreationDate = new DateTime(2033, 5, 21),
                MyProducts =  new List<Product>() {products[0], products[0], products[0] }
            },

            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customers[1].Id,
                Customer = customers[1],
                EmployeeId = employees[1].Id,
                Employee = employees[1],
                OrderNumber = 435,
                State = OrderState.Ready,
                TTN = 234,
                CreationDate = new DateTime(2000, 3, 11),
                MyProducts = new List<Product> { products[1] }
            },

            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customers[2].Id,
                Customer = customers[2],
                EmployeeId = employees[2].Id,
                Employee = employees[2],
                OrderNumber = 342,
                State = OrderState.Ready,
                TTN = 65422,
                CreationDate = new DateTime(2322, 2, 1),
                MyProducts = new List<Product> { products[2] }
            }
        };


        [Fact]
        public async void CreateOrderTest()
        {
            var mock = new Mock<IOrderUnitOfWork>();

            mock.Setup(_unitOfWork => _unitOfWork.CustomerRepository.GetByIdLink(customers[0].Id))
                .ReturnsAsync(customers[0]);

            mock.Setup(_unitOfWork => _unitOfWork.OrderRepository.Add(null));

            mock.Setup(_unitOfWork => _unitOfWork.ProductServiceRepository.GetById(services[0].Id))
                .ReturnsAsync(services[0]);

            mock.Setup(_unitOfWork => _unitOfWork.ProductRepository.Add(null));

            mock.Setup(_unitOfWork => _unitOfWork.Save());

            OrderProcessingService service = new OrderProcessingService(mock.Object, null);

            var order = await service.CreateOrder(customers[0].Id, new List<Product> { products[0], products[0], products[0] }, new List<OrderComment>());

           
            foreach (Product product in order.MyProducts)
            {
                Assert.Equal(product.ContainingOrder, order);
                Assert.Equal(product.ServiceId, products[0].ServiceId);
                Assert.Equal(product.Amount, products[0].Amount);
                Assert.Equal(product.Picture, products[0].Picture);
            }

            Assert.Equal(order.MyProducts, new List<Product> { products[0], products[0], products[0] });
            Assert.Equal(order.Customer, customers[0]);
            Assert.Null(order.Employee);
            Assert.False(order.IsCanceled);
            Assert.Equal(OrderState.New, order.State);
            Assert.Equal(0, order.TTN);
        }

        [Fact]
        public async void CancelOrderTest()
        {
            var mock = new Mock<IOrderUnitOfWork>();

            mock.Setup(_unitOfWork => _unitOfWork.CustomerRepository.GetByIdLinkOrDefault(customers[0].Id))
                .ReturnsAsync(customers[0]);

            mock.Setup(_unitOfWork => _unitOfWork.EmployeeRepository.GetByIdLinkOrDefault(employees[0].Id))
                .ReturnsAsync(employees[0]);

            mock.Setup(_unitOfWork => _unitOfWork.OrderRepository.GetById(orders[0].Id))
                .ReturnsAsync(orders[0]);

            mock.Setup(_unitOfWork => _unitOfWork.Save());

            OrderProcessingService serviceCust = new OrderProcessingService(mock.Object, null);
            var orderCust = await serviceCust.CancelOrder(orders[0].Id, customers[0].Id);

            OrderProcessingService serviceEmp = new OrderProcessingService(mock.Object, null);
            var orderEmp = await serviceEmp.CancelOrder(orders[0].Id, employees[0].Id);

            Assert.True(orderCust.IsCanceled);
            Assert.True(orderEmp.IsCanceled);
        }

        [Fact]
        public async void StartWorkingWithOrderTest()
        {
            var mock = new Mock<IOrderUnitOfWork>();

            mock.Setup(_unitOfWork => _unitOfWork.OrderRepository.GetById(orders[0].Id))
                .ReturnsAsync(orders[0]);

            mock.Setup(_unitOfWork => _unitOfWork.EmployeeRepository.GetByIdLinkOrDefault(employees[0].Id))
                .ReturnsAsync(employees[0]);

            mock.Setup(_unitOfWork => _unitOfWork.Save());

            OrderProcessingService service = new OrderProcessingService(mock.Object, _mapper);

            var orderModel = await service.StartWorkingWithOrder<EmployeeRoleDetailOrderInfoModel>(orders[0].Id, employees[0].Id);

            Assert.Equal(orderModel.State, OrderState.InProgress.ToString());
            Assert.NotEmpty(orderModel.Products);
            Assert.Equal(0, orderModel.TTN);
        }

        [Fact]
        public async void SetOrderTTNTest()
        {
            var mock = new Mock<IOrderUnitOfWork>();

            mock.Setup(_unitOfWork => _unitOfWork.OrderRepository.GetById(orders[0].Id))
                .ReturnsAsync(orders[0]);

            mock.Setup(_unitOfWork => _unitOfWork.Save());


            OrderProcessingService service = new OrderProcessingService(mock.Object, null);

            var order = await service.SetOrderTTN(orders[0].Id, orders[0].TTN);

            Assert.Equal(order.TTN, orders[0].TTN);
            Assert.Equal(OrderState.Ready, order.State);
        }

        [Fact]
        public async void GetProductsTest()
        {
            var mock = new Mock<IOrderUnitOfWork>();
            products[0].OrderId = orders[0].Id;

            mock.Setup(_unitOfWork => _unitOfWork.ProductRepository.GetByOrderId(orders[0].Id))
                .ReturnsAsync(new List<Product> { products[0] });

            mock.Setup(_unitOfWork => _unitOfWork.Save());

            OrderProcessingService service = new OrderProcessingService(mock.Object, null);

            var _products = await service.GetProducts(orders[0].Id);

            foreach (Product product in _products)
            {
                Assert.Equal(orders[0].Id, product.OrderId);
            }

        }
    }
}
