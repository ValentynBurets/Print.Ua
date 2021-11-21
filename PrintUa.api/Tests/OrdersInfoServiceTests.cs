using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Business;
using AutoMapper;
using Domain.Entity;
using Data.Interface.UnitOfWork;
using Domain.Repository.OrderManagement;
using Api.Configurations;
using Api.Models.OrderController.OrderInfoModels;
using Api.Models.OrderController.DetailOrderInfoModels;

namespace BusinessTests
{
    public class OrdersInfoServiceTests
    {
        private Mapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MapperInitializer())));

        private static List<Customer> customersInitData = new List<Customer>
        {
            new Customer(Guid.NewGuid()) { Id = Guid.NewGuid(), Name = "CustomerName1", Surname = "CustomerSurname1" },
            new Customer(Guid.NewGuid()) { Id = Guid.NewGuid(), Name = "CustomerName2", Surname = "CustomerSurname2" },
            new Customer(Guid.NewGuid()) { Id = Guid.NewGuid(), Name = "CustomerName3", Surname = "CustomerSurname3" }
        };

        private static List<Employee> employeesInitData = new List<Employee>
        {
            new Employee(Guid.NewGuid()) { Id = Guid.NewGuid(), Name = "EmployeeName1", Surname = "EmployeeSurname1" },
            new Employee(Guid.NewGuid()) { Id = Guid.NewGuid(), Name = "EmployeeName2", Surname = "EmployeeSurname2" },
            new Employee(Guid.NewGuid()) { Id = Guid.NewGuid(), Name = "EmployeeName3", Surname = "EmployeeSurname3" },
        };

        private static List<ServiceFormat> serviceFormatsInitData = new List<ServiceFormat>
        {
            new ServiceFormat { Id = Guid.NewGuid(), FormatName = "Format1" },
            new ServiceFormat { Id = Guid.NewGuid(), FormatName = "Format2" },
            new ServiceFormat { Id = Guid.NewGuid(), FormatName = "Format3" }
        };

        private static List<ServiceMaterial> serviceMaterialsInitData = new List<ServiceMaterial>
        {
            new ServiceMaterial { Id = Guid.NewGuid(), MaterialName = "Material1" },
            new ServiceMaterial { Id = Guid.NewGuid(), MaterialName = "Material2" },
            new ServiceMaterial { Id = Guid.NewGuid(), MaterialName = "Material3" }
        };

        private static List<ProductService> servicesInitDate = new List<ProductService>
        {
            new ProductService { Id = Guid.NewGuid(), Name = "Service1", Cost = 100, Format = serviceFormatsInitData[0], Material = serviceMaterialsInitData[0] },
            new ProductService { Id = Guid.NewGuid(), Name = "Service2", Cost = 200, Format = serviceFormatsInitData[1], Material = serviceMaterialsInitData[1] },
            new ProductService { Id = Guid.NewGuid(), Name = "Service3", Cost = 300, Format = serviceFormatsInitData[2], Material = serviceMaterialsInitData[2] }
        };

        private static List<Product> productsInitData = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Picture = new byte[] { 1, 2, 3 }, Amount = 1, ServiceInProduct = servicesInitDate[0] },
            new Product { Id = Guid.NewGuid(), Picture = new byte[] { 4, 5, 6 }, Amount = 2, ServiceInProduct = servicesInitDate[1] },
            new Product { Id = Guid.NewGuid(), Picture = new byte[] { 7, 8, 9 }, Amount = 3, ServiceInProduct = servicesInitDate[2] }
        };

        private static List<Order> ordersInitData = new List<Order>
        {
            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customersInitData[0].Id,
                Customer = customersInitData[0],
                EmployeeId = employeesInitData[0].Id,
                Employee = employeesInitData[0],
                OrderNumber = 111,
                State = OrderState.Ready,
                TTN = 443,
                CreationDate = new DateTime(2033, 5, 21),
                MyProducts = new List<Product> { productsInitData[0] }
            },

            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customersInitData[1].Id,
                Customer = customersInitData[1],
                EmployeeId = employeesInitData[1].Id,
                Employee = employeesInitData[1],
                OrderNumber = 435,
                State = OrderState.Ready,
                TTN = 234,
                CreationDate = new DateTime(2000, 3, 11),
                MyProducts = new List<Product> { productsInitData[1] }
            },

            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customersInitData[2].Id,
                Customer = customersInitData[2],
                EmployeeId = employeesInitData[2].Id,
                Employee = employeesInitData[2],
                OrderNumber = 342,
                State = OrderState.Ready,
                TTN = 65422,
                CreationDate = new DateTime(2322, 2, 1),
                MyProducts = new List<Product> { productsInitData[2] }
            }
        };


        //[Fact]
        //async void AdminRoleGetAllOrdersInRepositoryEqualOrdersModels()
        //{
        //    // Arrange

        //    var orderRepositoryMock = new Mock<IOrderRepository>();

        //    orderRepositoryMock
        //        .Setup(obj => obj.GetAll())
        //        .ReturnsAsync(ordersInitData).Verifiable();

        //    var unitOfWorkMock = new Mock<IOrderUnitOfWork>();

        //    unitOfWorkMock.Setup(obj => obj.OrderRepository)
        //        .Returns(orderRepositoryMock.Object).Verifiable();

        //    OrdersInfoService service = new OrdersInfoService(unitOfWorkMock.Object, mapper);

        //    // Act

        //    var expected = await service.AdminRoleGetAll<AdminRoleOrderInfoModel>();

        //    // Assert

        //    var expectedOrders = expected.ToList();

        //    Assert.Equal(ordersInitData.Count, expectedOrders.Count);

        //    for (int i = 0; i < ordersInitData.Count; i++)
        //    {
        //        Assert.Equal(ordersInitData[i].Id, expectedOrders[i].Id);
        //        Assert.Equal(ordersInitData[i].OrderNumber, expectedOrders[i].OrderNumber);
        //        Assert.Equal(ordersInitData[i].State.ToString(), expectedOrders[i].State);
        //        Assert.Equal($"{ordersInitData[i].Employee.Name} {ordersInitData[i].Employee.Surname}", expectedOrders[i].EmployeeName);
        //        Assert.Equal($"{ordersInitData[i].Customer.Name} {ordersInitData[i].Customer.Surname}", expectedOrders[i].CustomerName);
        //    }
        //}

        //[Fact]
        //async void CustomerRoleGetAllOrdersCountEqualCustomerHasOrders()
        //{
        //    // Arrange

        //    Guid customerId = customersInitData[0].Id;

        //    var orderRepositoryMock = new Mock<IOrderRepository>();

        //    orderRepositoryMock
        //        .Setup(obj => obj.GetByCustomerId(customerId))
        //        .ReturnsAsync(ordersInitData.Where(o => o.CustomerId == customerId));

        //    var unitOfWorkMock = new Mock<IOrderUnitOfWork>();

        //    unitOfWorkMock.Setup(obj => obj.OrderRepository)
        //        .Returns(orderRepositoryMock.Object);

        //    OrdersInfoService service = new OrdersInfoService(unitOfWorkMock.Object, mapper);

        //    // Act

        //    var expected = await service.CustomerRoleGetAll<CustomerRoleOrderInfoModel>(customerId);

        //    // Assert

        //    var expectedList = expected.ToList();

        //    Assert.Equal(expectedList.Count, 1);

        //    Assert.Equal(expectedList[0].Id, ordersInitData[0].Id);
        //    Assert.Equal(expectedList[0].OrderNumber, ordersInitData[0].OrderNumber);
        //    Assert.Equal(expectedList[0].State, ordersInitData[0].State.ToString());
        //    Assert.Equal(expectedList[0].EmployeeName, $"{ordersInitData[0].Employee.Name} {ordersInitData[0].Employee.Surname}");
        //}

        //[Fact]
        //async void EmployeeRoleGetAllOrdersCountEqualCustomerHasOrders()
        //{
        //    // Arrange

        //    Guid employeeId = employeesInitData[0].Id;

        //    var orderRepositoryMock = new Mock<IOrderRepository>();

        //    orderRepositoryMock
        //        .Setup(obj => obj.GetByEmployeeId(employeeId))
        //        .ReturnsAsync(ordersInitData.Where(o => o.EmployeeId == employeeId));

        //    var unitOfWorkMock = new Mock<IOrderUnitOfWork>();

        //    unitOfWorkMock.Setup(obj => obj.OrderRepository)
        //        .Returns(orderRepositoryMock.Object);

        //    OrdersInfoService service = new OrdersInfoService(unitOfWorkMock.Object, mapper);

        //    // Act

        //    var expected = await service.EmployeeRoleGetAll<EmployeeRoleOrderInfoModel>(employeeId);

        //    // Assert

        //    var expectedList = expected.ToList();

        //    Assert.Equal(expectedList.Count, 1);

        //    Assert.Equal(expectedList[0].Id, ordersInitData[0].Id);
        //    Assert.Equal(expectedList[0].OrderNumber, ordersInitData[0].OrderNumber);
        //    Assert.Equal(expectedList[0].State, ordersInitData[0].State.ToString());
        //    Assert.Equal(expectedList[0].CustomerName, $"{ordersInitData[0].Customer.Name} {ordersInitData[0].Customer.Surname}");
        //}

        //[Fact]
        //async void AdminRoleGetByIdOrderInRepositoryEqualOrderModel()
        //{
        //    // Arrange

        //    Guid orderId = ordersInitData[0].Id;

        //    var orderRepositoryMock = new Mock<IOrderRepository>();

        //    orderRepositoryMock
        //        .Setup(obj => obj.GetById(orderId))
        //        .ReturnsAsync(ordersInitData.FirstOrDefault(o => o.Id == orderId));

        //    var unitOfWorkMock = new Mock<IOrderUnitOfWork>();

        //    unitOfWorkMock.Setup(obj => obj.OrderRepository)
        //        .Returns(orderRepositoryMock.Object);

        //    OrdersInfoService service = new OrdersInfoService(unitOfWorkMock.Object, mapper);

        //    // Act

        //    var expected = await service.AdminRoleGetById<AdminRoleDetailOrderInfoModel>(orderId);

        //    // Assert

        //    Assert.NotNull(expected);

        //    Assert.Equal(expected.Id, ordersInitData[0].Id);
        //    Assert.Equal(expected.OrderNumber, ordersInitData[0].OrderNumber);
        //    Assert.Equal(expected.CreationDate, ordersInitData[0].CreationDate);
        //    Assert.Equal(expected.TTN, ordersInitData[0].TTN);
        //    Assert.Equal(expected.State, ordersInitData[0].State.ToString());
        //    Assert.Equal(expected.Customer.Id, ordersInitData[0].Customer.Id);
        //    Assert.Equal(expected.Customer.Name, ordersInitData[0].Customer.Name);
        //    Assert.Equal(expected.Customer.Surname, ordersInitData[0].Customer.Surname);
        //    Assert.Equal(expected.Employee.Id, ordersInitData[0].Employee.Id);
        //    Assert.Equal(expected.Employee.Name, ordersInitData[0].Employee.Name);
        //    Assert.Equal(expected.Employee.Surname, ordersInitData[0].Employee.Surname);
        //    Assert.Equal(expected.Products.Count, ordersInitData[0].MyProducts.Count);

        //    var expectedProductList = expected.Products.ToList();
        //    var initDataProductList = ordersInitData[0].MyProducts.ToList();

        //    Assert.Equal(expectedProductList[0].Id, initDataProductList[0].Id);
        //    Assert.Equal(expectedProductList[0].Picture, initDataProductList[0].Picture);
        //    Assert.Equal(expectedProductList[0].Amount, initDataProductList[0].Amount);
        //    Assert.Equal(expectedProductList[0].Service.Id, initDataProductList[0].ServiceInProduct.Id);
        //    Assert.Equal(expectedProductList[0].Service.Name, initDataProductList[0].ServiceInProduct.Name);
        //    Assert.Equal(expectedProductList[0].Service.Cost, initDataProductList[0].ServiceInProduct.Cost);
        //    Assert.Equal(expectedProductList[0].Service.Format.Id, initDataProductList[0].ServiceInProduct.Format.Id);
        //    Assert.Equal(expectedProductList[0].Service.Format.Name, initDataProductList[0].ServiceInProduct.Format.FormatName);
        //    Assert.Equal(expectedProductList[0].Service.Material.Id, initDataProductList[0].ServiceInProduct.Material.Id);
        //    Assert.Equal(expectedProductList[0].Service.Material.Name, initDataProductList[0].ServiceInProduct.Material.MaterialName);
        //}

        //[Fact]
        //async void CustomerRoleGetByIdOrderInRepositoryEqualOrderModel()
        //{
        //    // Arrange

        //    Guid orderId = ordersInitData[0].Id;
        //    Guid customerId = customersInitData[0].Id;

        //    var orderRepositoryMock = new Mock<IOrderRepository>();

        //    orderRepositoryMock
        //        .Setup(obj => obj.GetById(orderId))
        //        .ReturnsAsync(ordersInitData.FirstOrDefault(o => o.Id == orderId));

        //    var unitOfWorkMock = new Mock<IOrderUnitOfWork>();

        //    unitOfWorkMock.Setup(obj => obj.OrderRepository)
        //        .Returns(orderRepositoryMock.Object);

        //    OrdersInfoService service = new OrdersInfoService(unitOfWorkMock.Object, mapper);

        //    // Act

        //    var expected = await service.CustomerRoleGetById<CustomerRoleDetailOrderInfoModel>(customerId, orderId);

        //    // Assert

        //    Assert.NotNull(expected);

        //    Assert.Equal(expected.Id, ordersInitData[0].Id);
        //    Assert.Equal(expected.OrderNumber, ordersInitData[0].OrderNumber);
        //    Assert.Equal(expected.CreationDate, ordersInitData[0].CreationDate);
        //    Assert.Equal(expected.TTN, ordersInitData[0].TTN);
        //    Assert.Equal(expected.State, ordersInitData[0].State.ToString());
        //    Assert.Equal(expected.Employee.Id, ordersInitData[0].Employee.Id);
        //    Assert.Equal(expected.Employee.Name, ordersInitData[0].Employee.Name);
        //    Assert.Equal(expected.Employee.Surname, ordersInitData[0].Employee.Surname);
        //    Assert.Equal(expected.Products.Count, ordersInitData[0].MyProducts.Count);

        //    var expectedProductList = expected.Products.ToList();
        //    var initDataProductList = ordersInitData[0].MyProducts.ToList();

        //    Assert.Equal(expectedProductList[0].Id, initDataProductList[0].Id);
        //    Assert.Equal(expectedProductList[0].Picture, initDataProductList[0].Picture);
        //    Assert.Equal(expectedProductList[0].Amount, initDataProductList[0].Amount);
        //    Assert.Equal(expectedProductList[0].Service.Id, initDataProductList[0].ServiceInProduct.Id);
        //    Assert.Equal(expectedProductList[0].Service.Name, initDataProductList[0].ServiceInProduct.Name);
        //    Assert.Equal(expectedProductList[0].Service.Cost, initDataProductList[0].ServiceInProduct.Cost);
        //    Assert.Equal(expectedProductList[0].Service.Format.Id, initDataProductList[0].ServiceInProduct.Format.Id);
        //    Assert.Equal(expectedProductList[0].Service.Format.Name, initDataProductList[0].ServiceInProduct.Format.FormatName);
        //    Assert.Equal(expectedProductList[0].Service.Material.Id, initDataProductList[0].ServiceInProduct.Material.Id);
        //    Assert.Equal(expectedProductList[0].Service.Material.Name, initDataProductList[0].ServiceInProduct.Material.MaterialName);
        //}

        //[Fact]
        //async void EmployeeRoleGetByIdOrderInRepositoryEqualOrderModel()
        //{
        //    // Arrange

        //    Guid orderId = ordersInitData[0].Id;
        //    Guid employeeId = employeesInitData[0].Id;

        //    var orderRepositoryMock = new Mock<IOrderRepository>();

        //    orderRepositoryMock
        //        .Setup(obj => obj.GetById(orderId))
        //        .ReturnsAsync(ordersInitData.FirstOrDefault(o => o.Id == orderId));

        //    var unitOfWorkMock = new Mock<IOrderUnitOfWork>();

        //    unitOfWorkMock.Setup(obj => obj.OrderRepository)
        //        .Returns(orderRepositoryMock.Object);

        //    OrdersInfoService service = new OrdersInfoService(unitOfWorkMock.Object, mapper);

        //    // Act

        //    var expected = await service.EmployeeRoleGetById<EmployeeRoleDetailOrderInfoModel>(employeeId, orderId);

        //    // Assert

        //    Assert.NotNull(expected);

        //    Assert.Equal(expected.Id, ordersInitData[0].Id);
        //    Assert.Equal(expected.OrderNumber, ordersInitData[0].OrderNumber);
        //    Assert.Equal(expected.CreationDate, ordersInitData[0].CreationDate);
        //    Assert.Equal(expected.TTN, ordersInitData[0].TTN);
        //    Assert.Equal(expected.State, ordersInitData[0].State.ToString());
        //    Assert.Equal(expected.Customer.Id, ordersInitData[0].Customer.Id);
        //    Assert.Equal(expected.Customer.Name, ordersInitData[0].Customer.Name);
        //    Assert.Equal(expected.Customer.Surname, ordersInitData[0].Customer.Surname);
        //    Assert.Equal(expected.Products.Count, ordersInitData[0].MyProducts.Count);

        //    var expectedProductList = expected.Products.ToList();
        //    var initDataProductList = ordersInitData[0].MyProducts.ToList();

        //    Assert.Equal(expectedProductList[0].Id, initDataProductList[0].Id);
        //    Assert.Equal(expectedProductList[0].Picture, initDataProductList[0].Picture);
        //    Assert.Equal(expectedProductList[0].Amount, initDataProductList[0].Amount);
        //    Assert.Equal(expectedProductList[0].Service.Id, initDataProductList[0].ServiceInProduct.Id);
        //    Assert.Equal(expectedProductList[0].Service.Name, initDataProductList[0].ServiceInProduct.Name);
        //    Assert.Equal(expectedProductList[0].Service.Cost, initDataProductList[0].ServiceInProduct.Cost);
        //    Assert.Equal(expectedProductList[0].Service.Format.Id, initDataProductList[0].ServiceInProduct.Format.Id);
        //    Assert.Equal(expectedProductList[0].Service.Format.Name, initDataProductList[0].ServiceInProduct.Format.FormatName);
        //    Assert.Equal(expectedProductList[0].Service.Material.Id, initDataProductList[0].ServiceInProduct.Material.Id);
        //    Assert.Equal(expectedProductList[0].Service.Material.Name, initDataProductList[0].ServiceInProduct.Material.MaterialName);
        //}
    }
}
