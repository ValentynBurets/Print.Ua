using Api.Configurations;
using AutoMapper;
using Business.Services;
using Data.Interface.UnitOfWork;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Data;
using Xunit;

namespace BusinessTests
{
    public class ProfileRegistrationServiceTest
    {
        private static IMapper _mapper;

        private static readonly SystemUser systemUserElem =new SystemUser();

        private static readonly List<List<string>> roleList = new() { new() { "Customer" }, new() { "Employee" }, new() { "Admin" }, new() { "Not"} };

        public ProfileRegistrationServiceTest()
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

        [Theory]
        [InlineData(0, "Nikita", "Strongin")]
        [InlineData(1, "Nikita2", "Strongin2")]
        [InlineData(2, "Nikita3", "Strongin3")]
        public async void CreateProfileTest(int id, string Name, string LastName)
        {
            //Average
            var mockIOrderUnitOfWork = new Mock<IOrderUnitOfWork>();

            var store = new Mock<IUserStore<SystemUser>>();

            var mockUserManager = new Mock<UserManager<SystemUser>>(store.Object, null, null, null, null, null, null, null, null);

            mockUserManager.Setup(systemUser => systemUser.GetRolesAsync(It.IsAny<SystemUser>())).ReturnsAsync(roleList[id]);

            mockIOrderUnitOfWork.Setup(mockIOrderUnitOfWork => mockIOrderUnitOfWork.CustomerRepository.Add(It.IsAny<Customer>()));
            mockIOrderUnitOfWork.Setup(mockIOrderUnitOfWork => mockIOrderUnitOfWork.EmployeeRepository.Add(It.IsAny<Employee>()));
            mockIOrderUnitOfWork.Setup(mockIOrderUnitOfWork => mockIOrderUnitOfWork.AdminRepository.Add(It.IsAny<Admin>()));

            mockIOrderUnitOfWork.Setup(mockIOrderUnitOfWork => mockIOrderUnitOfWork.Save());

            ProfileRegistrationService profileRegistrationService = new(mockUserManager.Object, mockIOrderUnitOfWork.Object);
            //Act
            var result = await profileRegistrationService.CreateProfile(systemUserElem, Name, LastName);
            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("Nikita", "Strongin")]
        public async Task CreateProfileTestFalse(string Name, string LastName)
        {
            //Average
            var mockIOrderUnitOfWork = new Mock<IOrderUnitOfWork>();

            var store = new Mock<IUserStore<SystemUser>>();

            string expectedMessege = "Role is not set";

            var mockUserManager = new Mock<UserManager<SystemUser>>(store.Object, null, null, null, null, null, null, null, null);

            mockUserManager.Setup(systemUser => systemUser.GetRolesAsync(It.IsAny<SystemUser>())).ReturnsAsync(new List<string> { });

            ProfileRegistrationService profileRegistrationService = new(mockUserManager.Object, mockIOrderUnitOfWork.Object);
            //Act
            Action act = () => profileRegistrationService.CreateProfile(systemUserElem, Name, LastName);
            //Assert
            Exception exception = await Assert.ThrowsAsync<Exception>(() => profileRegistrationService.CreateProfile(systemUserElem, Name, LastName));
            Assert.Equal(expectedMessege, exception.Message);
        }
    }
}
