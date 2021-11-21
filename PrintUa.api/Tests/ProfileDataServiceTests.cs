using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Business.Services;
using Domain.Entity;
using Business.Interface.Models;
using Microsoft.AspNetCore.Identity;
using UserIdentity.Data;
using Data.UnitOfWork;
using AutoMapper;
using Data.Repository;
using Domain.Repository.UserManagement;
using Data.Interface.UnitOfWork;

namespace BusinessTests
{
    public class ProfileDataServiceTests
    {
        [Fact]
        public async void GetCustomerProfileInfoById_CustomerId_EqualCustomerProfileInfoModel()
        {
            // Arrange
            Customer testedCustomer = new Customer(new Guid()) { Name = "Ivan", Surname = "Rudskoy"};

            var customerRepositoryStub = new Mock<ICustomerRepository>();
            customerRepositoryStub.Setup(obj => obj.GetByIdLink(It.IsAny<Guid>()))
                .ReturnsAsync(testedCustomer);

            var orderUnitOfWorkStub = new Mock<IOrderUnitOfWork>();
            orderUnitOfWorkStub.Setup(obj => obj.CustomerRepository)
                .Returns(customerRepositoryStub.Object);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Customer, ProfileInfoModel>());
            var mapper = new Mapper(mapperConfiguration);

            ProfileDataService ProfileDataService = new ProfileDataService(orderUnitOfWorkStub.Object, mapper);

            // Act

            ProfileInfoModel expect = await ProfileDataService.GetCustomerProfileInfoById(testedCustomer.Id);

            // Assert
            Assert.NotNull(expect);
            Assert.Equal(expect.Name, testedCustomer.Name);
            Assert.Equal(expect.Surname, testedCustomer.Surname);
        }

        [Fact]
        public async void GetEmployeeProfileInfoById_CustomerId_EqualCustomerProfileInfoModel()
        {
            // Arrange
            Employee testedEmployee = new Employee(new Guid()) { Name = "Ivan", Surname = "Rudskoy" };

            var employeeRepositoryStub = new Mock<IEmployeeRepository>();
            employeeRepositoryStub.Setup(obj => obj.GetByIdLink(It.IsAny<Guid>()))
                .ReturnsAsync(testedEmployee);

            var orderUnitOfWorkStub = new Mock<IOrderUnitOfWork>();
            orderUnitOfWorkStub.Setup(obj => obj.EmployeeRepository)
                .Returns(employeeRepositoryStub.Object);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Employee, ProfileInfoModel>());
            var mapper = new Mapper(mapperConfiguration);

            ProfileDataService ProfileDataService = new ProfileDataService(orderUnitOfWorkStub.Object, mapper);

            // Act

            ProfileInfoModel expect = await ProfileDataService.GetEmployeeProfileInfoById(testedEmployee.Id);

            // Assert
            Assert.NotNull(expect);
            Assert.Equal(expect.Name, testedEmployee.Name);
            Assert.Equal(expect.Surname, testedEmployee.Surname);
        }

        [Fact]
        public async void GetAdminProfileInfoById_CustomerId_EqualCustomerProfileInfoModel()
        {
            // Arrange
            Admin testedAdmin = new Admin(new Guid()) { Name = "Ivan", Surname = "Rudskoy" };

            var adminRepositoryStub = new Mock<IAdminRepository>();
            adminRepositoryStub.Setup(obj => obj.GetByIdLink(It.IsAny<Guid>()))
                .ReturnsAsync(testedAdmin);

            var orderUnitOfWorkStub = new Mock<IOrderUnitOfWork>();
            orderUnitOfWorkStub.Setup(obj => obj.AdminRepository)
                .Returns(adminRepositoryStub.Object);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Admin, ProfileInfoModel>());
            var mapper = new Mapper(mapperConfiguration);

            ProfileDataService ProfileDataService = new ProfileDataService(orderUnitOfWorkStub.Object, mapper);

            // Act

            ProfileInfoModel expect = await ProfileDataService.GetAdminProfileInfoById(testedAdmin.Id);

            // Assert
            Assert.NotNull(expect);
            Assert.Equal(expect.Name, testedAdmin.Name);
            Assert.Equal(expect.Surname, testedAdmin.Surname);
        }

        [Fact]
        public async void UpdateCustomerProfileInfoById_ProfileInfoModelAndCustomerId_UpdatedCustomer()
        {
            // Arrange
            Customer testedCustomer = new Customer(new Guid()) { Name = "Ivan", Surname = "Rudskoy" };
            ProfileInfoModel newProfileInfo = new ProfileInfoModel { Name = "Vasya", Surname = "Vlasov" };

            var customerRepositoryStub = new Mock<ICustomerRepository>();
            customerRepositoryStub.Setup(obj => obj.GetByIdLink(It.IsAny<Guid>()))
                .ReturnsAsync(testedCustomer);

            var orderUnitOfWorkStub = new Mock<IOrderUnitOfWork>();
            orderUnitOfWorkStub.Setup(obj => obj.CustomerRepository)
                .Returns(customerRepositoryStub.Object);

            var mapperFake = new Mock<IMapper>();

            ProfileDataService ProfileDataService = new ProfileDataService(orderUnitOfWorkStub.Object, mapperFake.Object);

            // Act

             await ProfileDataService.UpdateCustomerProfileInfoById(newProfileInfo, testedCustomer.Id);

            // Assert
            Assert.Equal(testedCustomer.Name, newProfileInfo.Name);
            Assert.Equal(testedCustomer.Surname, newProfileInfo.Surname);
        }

        [Fact]
        public async void UpdateEmployeeProfileInfoById_ProfileInfoModelAndCustomerId_UpdatedCustomer()
        {
            // Arrange
            Employee testedEmployee = new Employee(new Guid()) { Name = "Ivan", Surname = "Rudskoy" };
            ProfileInfoModel newProfileInfo = new ProfileInfoModel { Name = "Vasya", Surname = "Vlasov" };

            var employeeRepositoryStub = new Mock<IEmployeeRepository>();
            employeeRepositoryStub.Setup(obj => obj.GetByIdLink(It.IsAny<Guid>()))
                .ReturnsAsync(testedEmployee);

            var orderUnitOfWorkStub = new Mock<IOrderUnitOfWork>();
            orderUnitOfWorkStub.Setup(obj => obj.EmployeeRepository)
                .Returns(employeeRepositoryStub.Object);

            var mapperFake = new Mock<IMapper>();

            ProfileDataService ProfileDataService = new ProfileDataService(orderUnitOfWorkStub.Object, mapperFake.Object);

            // Act

            await ProfileDataService.UpdateEmployeeProfileInfoById(newProfileInfo, testedEmployee.Id);

            // Assert
            Assert.Equal(testedEmployee.Name, newProfileInfo.Name);
            Assert.Equal(testedEmployee.Surname, newProfileInfo.Surname);
        }

        [Fact]
        public async void UpdateAdminProfileInfoById_ProfileInfoModelAndCustomerId_UpdatedCustomer()
        {
            // Arrange
            Admin testedAdmin = new Admin(new Guid()) { Name = "Ivan", Surname = "Rudskoy" };
            ProfileInfoModel newProfileInfo = new ProfileInfoModel { Name = "Vasya", Surname = "Vlasov" };

            var adminRepositoryStub = new Mock<IAdminRepository>();
            adminRepositoryStub.Setup(obj => obj.GetByIdLink(It.IsAny<Guid>()))
                .ReturnsAsync(testedAdmin);

            var orderUnitOfWorkStub = new Mock<IOrderUnitOfWork>();
            orderUnitOfWorkStub.Setup(obj => obj.AdminRepository)
                .Returns(adminRepositoryStub.Object);

            var mapperFake = new Mock<IMapper>();

            ProfileDataService ProfileDataService = new ProfileDataService(orderUnitOfWorkStub.Object, mapperFake.Object);

            // Act

            await ProfileDataService.UpdateAdminProfileInfoById(newProfileInfo, testedAdmin.Id);

            // Assert
            Assert.Equal(testedAdmin.Name, newProfileInfo.Name);
            Assert.Equal(testedAdmin.Surname, newProfileInfo.Surname);
        }
    }
}
