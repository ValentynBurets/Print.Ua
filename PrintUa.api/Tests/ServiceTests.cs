using Api.Configurations;
using AutoMapper;
using Business.Contract.Model;
using Business.Model;
using Business.Service.Entity;
using Data.Interface.UnitOfWork;
using Domain.Entity;
using Domain.Repository.ProductManagement;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BusinessTests
{
    public class ServiceTests
    {
        private Mapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MapperInitializer())));

        private static List<ProductService> serviceInitData = new List<ProductService>
        {
            new ProductService()
            {
                Id = new Guid(),
                Name = "Service Name",
                Cost = 100,
                IsCanceled = true,
                FormatId = new Guid("c49db605-95ac-449a-9aba-348569e5a852"),
                MaterialId = new Guid("c08ad180-b5b1-4b6e-8daf-f7231982424e")
            },
            new ProductService()
            {
                Id = new Guid(),
                Name = "Service Name2",
                Cost = 200,
                IsCanceled = false,
                FormatId = new Guid("bb38641a-21c5-481f-8053-d91ab46fbb27"),
                MaterialId = new Guid("b1962eca-acdf-4f5f-83e7-e814b53d8694")
            }
        };

        List<ServiceMaterial> materialList = new List<ServiceMaterial>()
        {
            new ServiceMaterial(){
                Id = new Guid("c08ad180-b5b1-4b6e-8daf-f7231982424e"),
                MaterialName = "Materail_1"
            },

            new ServiceMaterial(){
                Id = new Guid("b1962eca-acdf-4f5f-83e7-e814b53d8694"),
                MaterialName = "Materail_2"
            }
        };

        List<ServiceFormat> formatList = new List<ServiceFormat>()
        {
            new ServiceFormat(){
                Id = new Guid("c49db605-95ac-449a-9aba-348569e5a852"),
                FormatName = "Format_1"
            },
            
            new ServiceFormat(){
                Id = new Guid("bb38641a-21c5-481f-8053-d91ab46fbb27"),
                FormatName = "Format_2"
            }
        };

        CreateServiceViewModel testedCreateExistService = new CreateServiceViewModel()
        {
            Name = "Service Name",
            Cost = 100,
            MaterialName = "Materail_1",
            FormatName = "Format_1"
        };

        CreateServiceViewModel testedCreateService = new CreateServiceViewModel()
        {
            Name = "Service",
            Cost = 0,
            MaterialName = "Materail Name",
            FormatName = "Format Name"
        };

        UpdateServiceViewModel testedUpdateService = new UpdateServiceViewModel()
        {
            Name = "Service",
            Cost = 0,
            MaterialName = "Materail_1",
            FormatName = "Format_1"
        };

        ProductService nullService = null;

        ServiceMaterial serviceMaterial = new ServiceMaterial()
        {
            Id = new Guid(),
            MaterialName = "Materail Name"
        };

        ServiceFormat serviceFormat = new ServiceFormat()
        {
            Id = new Guid(),
            FormatName = "Format Name"
        };




        [Fact]
        public void CreateServiceTest()
        {
            // Arrange
            var service = serviceInitData[0];

            var productServiceRepositoryStub = new Mock<IProductServiceRepository>();
            productServiceRepositoryStub.Setup(obj => obj.GetByName(service.Name))
                .ReturnsAsync(service);
            productServiceRepositoryStub.Setup(obj => obj.GetByMaterialAndFormat(serviceMaterial.Id, serviceFormat.Id))
                .ReturnsAsync(nullService);


            var productServiceMaterialRepositoryStub = new Mock<IProductServiceMaterialRepository>();
            productServiceMaterialRepositoryStub.Setup(obj => obj.GetByMaterialName(serviceMaterial.MaterialName))
                .ReturnsAsync(serviceMaterial);

            var productServiceFormatRepositoryStub = new Mock<IProductServiceFormatRepository>();
            productServiceFormatRepositoryStub.Setup(obj => obj.GetByFormatName(serviceFormat.FormatName))
                .ReturnsAsync(serviceFormat);

            //unit of wor initializationa
            var serviceUnitOfWorkStub = new Mock<IOrderUnitOfWork>();
            serviceUnitOfWorkStub.Setup(obj => obj.ProductServiceRepository)
                .Returns(productServiceRepositoryStub.Object);

            serviceUnitOfWorkStub.Setup(obj => obj.ProductServiceMaterialRepository)
                .Returns(productServiceMaterialRepositoryStub.Object);

            serviceUnitOfWorkStub.Setup(obj => obj.ProductServiceFormatRepository)
                .Returns(productServiceFormatRepositoryStub.Object);

            ProductServiceService productServiceService = new ProductServiceService(serviceUnitOfWorkStub.Object, mapper);

            // Act
            var result = productServiceService.Create(testedCreateService);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateExistsServiceTest()
        {
            // Arrange
            var service = serviceInitData[0];

            var productServiceRepositoryStub = new Mock<IProductServiceRepository>();
            productServiceRepositoryStub.Setup(obj => obj.GetByName(service.Name))
                .ReturnsAsync(service);
            productServiceRepositoryStub.Setup(obj => obj.GetByMaterialAndFormat(serviceMaterial.Id, serviceFormat.Id))
                .ReturnsAsync(serviceInitData[0]);


            var productServiceMaterialRepositoryStub = new Mock<IProductServiceMaterialRepository>();
            productServiceMaterialRepositoryStub.Setup(obj => obj.GetByMaterialName(serviceMaterial.MaterialName))
                .ReturnsAsync(serviceMaterial);

            var productServiceFormatRepositoryStub = new Mock<IProductServiceFormatRepository>();
            productServiceFormatRepositoryStub.Setup(obj => obj.GetByFormatName(serviceFormat.FormatName))
                .ReturnsAsync(serviceFormat);

            //unit of wor initializationa
            var serviceUnitOfWorkStub = new Mock<IOrderUnitOfWork>();
            serviceUnitOfWorkStub.Setup(obj => obj.ProductServiceRepository)
                .Returns(productServiceRepositoryStub.Object);

            serviceUnitOfWorkStub.Setup(obj => obj.ProductServiceMaterialRepository)
                .Returns(productServiceMaterialRepositoryStub.Object);

            serviceUnitOfWorkStub.Setup(obj => obj.ProductServiceFormatRepository)
                .Returns(productServiceFormatRepositoryStub.Object);

            ProductServiceService productServiceService = new ProductServiceService(serviceUnitOfWorkStub.Object, mapper);

            // Act
            var expectedResult = productServiceService.Create(testedCreateExistService);

            // Assert
            const string errorResult = "One or more errors occurred. (Service with this name exists)";
            Assert.Equal(expectedResult.Exception.Message.ToString(), errorResult);
        }

        [Fact]
        async void GetAllServicesTest()
        {
            //Arrange

            var serviceRepositoryStab = new Mock<IProductServiceRepository>();
            serviceRepositoryStab
                .Setup(obj => obj.GetAll())
                .ReturnsAsync(serviceInitData);

            var materialRepositoryStab = new Mock<IProductServiceMaterialRepository>();
            materialRepositoryStab
                .Setup(obj => obj.GetById(materialList[0].Id))
                .ReturnsAsync(materialList[0]);

            var formatRepositoryStab = new Mock<IProductServiceFormatRepository>();
            formatRepositoryStab
                .Setup(obj => obj.GetById(formatList[0].Id))
                .ReturnsAsync(formatList[0]);

            var unitOfWorkMock = new Mock<IOrderUnitOfWork>();

            unitOfWorkMock.Setup(obj => obj.ProductServiceRepository)
                .Returns(serviceRepositoryStab.Object);
            unitOfWorkMock.Setup(obj => obj.ProductServiceFormatRepository)
                .Returns(formatRepositoryStab.Object);
            unitOfWorkMock.Setup(obj => obj.ProductServiceMaterialRepository)
                .Returns(materialRepositoryStab.Object);

            ProductServiceService service = new ProductServiceService(unitOfWorkMock.Object, mapper);

            // Act

            var expected = await service.GetAll();

            //Assert

            var resultList = expected.ToList();

            Assert.NotNull(resultList);
            Assert.Equal(serviceInitData[0].Id, resultList[0].Id);
            Assert.Equal(serviceInitData[0].MaterialId, resultList[0].Material.Id);
            Assert.Equal(serviceInitData[0].Name, resultList[0].Name);
            Assert.Equal(serviceInitData[0].FormatId, resultList[0].Format.Id);
        }

        [Fact]
        public void GetServicesByIdTest()
        {
            // Arrange

            var serviceInfo = mapper.Map<ProductService, ServiceViewModel>(serviceInitData[0]);

            var serviceRepositoryStub = new Mock<IProductServiceRepository>();
            serviceRepositoryStub
                .Setup(obj => obj.GetById(serviceInitData[0].Id))
                .ReturnsAsync(serviceInitData[0]);

            var materialRepositoryStab = new Mock<IProductServiceMaterialRepository>();
            materialRepositoryStab
                .Setup(obj => obj.GetById(materialList[0].Id))
                .ReturnsAsync(materialList[0]);

            var formatRepositoryStab = new Mock<IProductServiceFormatRepository>();
            formatRepositoryStab
                .Setup(obj => obj.GetById(formatList[0].Id))
                .ReturnsAsync(formatList[0]);


            var serviceUnitOfWorkStub = new Mock<IOrderUnitOfWork>();
            serviceUnitOfWorkStub.Setup(obj => obj.ProductServiceRepository)
                .Returns(serviceRepositoryStub.Object);
            serviceUnitOfWorkStub.Setup(obj => obj.ProductServiceMaterialRepository)
                .Returns(materialRepositoryStab.Object);
            serviceUnitOfWorkStub.Setup(obj => obj.ProductServiceFormatRepository)
                .Returns(formatRepositoryStab.Object);


            ProductServiceService productServiceService = new ProductServiceService(serviceUnitOfWorkStub.Object, mapper);

            // Act
            var received_service = productServiceService.GetById(serviceInitData[0].Id).Result;

            // Assert
            Assert.NotNull(received_service);
            Assert.Equal(serviceInitData[0].Name, received_service.Name);
        }

        [Fact]
        public void DeleteServicesByIdTest()
        {
            // Arrange

            var serviceInfo = mapper.Map<ProductService, ServiceViewModel>(serviceInitData[0]);

            var serviceRepositoryStub = new Mock<IProductServiceRepository>();
            serviceRepositoryStub
                .Setup(obj => obj.GetById(serviceInitData[0].Id))
                .ReturnsAsync(serviceInitData[0]);

            var serviceUnitOfWorkStub = new Mock<IOrderUnitOfWork>();
            serviceUnitOfWorkStub.Setup(obj => obj.ProductServiceRepository)
                .Returns(serviceRepositoryStub.Object);

            ProductServiceService productServiceService = new ProductServiceService(serviceUnitOfWorkStub.Object, mapper);

            // Act
            var result = productServiceService.Delete(serviceInitData[0].Id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateServicesByIdTest()
        {
            // Arrange

            var serviceUpdate = mapper.Map<ProductService, UpdateServiceViewModel>(serviceInitData[0]);

            serviceInitData[0].Material = materialList[0];
            serviceInitData[0].Format = formatList[0];

            var serviceRepositoryStub = new Mock<IProductServiceRepository>();
            serviceRepositoryStub
                .Setup(obj => obj.GetById(serviceInitData[0].Id))
                .ReturnsAsync(serviceInitData[0]);
            serviceRepositoryStub.Setup(obj => obj.GetByMaterialAndFormat(serviceInitData[0].Id, serviceInitData[0].Id))
                .ReturnsAsync(serviceInitData[0]);


            var materialRepositoryStab = new Mock<IProductServiceMaterialRepository>();
            materialRepositoryStab
                .Setup(obj => obj.GetById(materialList[0].Id))
                .ReturnsAsync(materialList[0]);
            materialRepositoryStab
                .Setup(obj => obj.GetByMaterialName(materialList[0].MaterialName))
                .ReturnsAsync(materialList[0]);

            var formatRepositoryStab = new Mock<IProductServiceFormatRepository>();
            formatRepositoryStab
                .Setup(obj => obj.GetById(formatList[0].Id))
                .ReturnsAsync(formatList[0]);
            formatRepositoryStab
                .Setup(obj => obj.GetByFormatName(formatList[0].FormatName))
                .ReturnsAsync(formatList[0]);


            var serviceUnitOfWorkStub = new Mock<IOrderUnitOfWork>();
            serviceUnitOfWorkStub.Setup(obj => obj.ProductServiceRepository)
                .Returns(serviceRepositoryStub.Object);
            serviceUnitOfWorkStub.Setup(obj => obj.ProductServiceMaterialRepository)
                .Returns(materialRepositoryStab.Object);
            serviceUnitOfWorkStub.Setup(obj => obj.ProductServiceFormatRepository)
                .Returns(formatRepositoryStab.Object);


            ProductServiceService productServiceService = new ProductServiceService(serviceUnitOfWorkStub.Object, mapper);

            // Act
            var result = productServiceService.Update(serviceInitData[0].Id, testedUpdateService);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void EnableServiceStateTest()
        {
            // Arrange

            var serviceInfo = mapper.Map<ProductService, ServiceViewModel>(serviceInitData[0]);

            var serviceRepositoryStub = new Mock<IProductServiceRepository>();
            serviceRepositoryStub
                .Setup(obj => obj.GetById(serviceInitData[0].Id))
                .ReturnsAsync(serviceInitData[0]);

            var serviceUnitOfWorkStub = new Mock<IOrderUnitOfWork>();
            serviceUnitOfWorkStub.Setup(obj => obj.ProductServiceRepository)
                .Returns(serviceRepositoryStub.Object);

            ProductServiceService productServiceService = new ProductServiceService(serviceUnitOfWorkStub.Object, mapper);

            // Act
            var result = productServiceService.EnableService(serviceInitData[0].Id);

            // Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void DisableServiceStateTest()
        {
            // Arrange

            var serviceInfo = mapper.Map<ProductService, ServiceViewModel>(serviceInitData[0]);

            var serviceRepositoryStub = new Mock<IProductServiceRepository>();
            serviceRepositoryStub
                .Setup(obj => obj.GetById(serviceInitData[0].Id))
                .ReturnsAsync(serviceInitData[0]);

            var serviceUnitOfWorkStub = new Mock<IOrderUnitOfWork>();
            serviceUnitOfWorkStub.Setup(obj => obj.ProductServiceRepository)
                .Returns(serviceRepositoryStub.Object);

            ProductServiceService productServiceService = new ProductServiceService(serviceUnitOfWorkStub.Object, mapper);

            // Act
            var result = productServiceService.DisableService(serviceInitData[0].Id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        async void GetAllNotCanceledServicesTest()
        {
            //Arrange

            var serviceRepositoryStab = new Mock<IProductServiceRepository>();
            serviceRepositoryStab
                .Setup(obj => obj.GetAllNotCanceled())
                .ReturnsAsync(new List<ProductService>() { serviceInitData[1] });

            var materialRepositoryStab = new Mock<IProductServiceMaterialRepository>();
            materialRepositoryStab
                .Setup(obj => obj.GetById(materialList[1].Id))
                .ReturnsAsync(materialList[1]);

            var formatRepositoryStab = new Mock<IProductServiceFormatRepository>();
            formatRepositoryStab
                .Setup(obj => obj.GetById(formatList[1].Id))
                .ReturnsAsync(formatList[1]);

            var unitOfWorkMock = new Mock<IOrderUnitOfWork>();

            unitOfWorkMock.Setup(obj => obj.ProductServiceRepository)
                .Returns(serviceRepositoryStab.Object);
            unitOfWorkMock.Setup(obj => obj.ProductServiceFormatRepository)
                .Returns(formatRepositoryStab.Object);
            unitOfWorkMock.Setup(obj => obj.ProductServiceMaterialRepository)
                .Returns(materialRepositoryStab.Object);

            ProductServiceService service = new ProductServiceService(unitOfWorkMock.Object, mapper);

            // Act

            var expectedResult = await service.GetAllNotCanceled();

            //Assert

            var expectedList = expectedResult.ToList();

            Assert.NotNull(expectedList);
            Assert.Equal(serviceInitData[1].Id, expectedList[0].Id);
            Assert.Equal(serviceInitData[1].MaterialId, expectedList[0].Material.Id);
            Assert.Equal(serviceInitData[1].Name, expectedList[0].Name);
            Assert.Equal(serviceInitData[1].FormatId, expectedList[0].Format.Id);
        }

        [Fact]
        async void GetDataForCreationTest()
        {
            //Arrange

            var materialRepositoryStab = new Mock<IProductServiceMaterialRepository>();
            materialRepositoryStab
                .Setup(obj => obj.GetAll())
                .ReturnsAsync(materialList);

            var formatRepositoryStab = new Mock<IProductServiceFormatRepository>();
            formatRepositoryStab
                .Setup(obj => obj.GetAll())
                .ReturnsAsync(formatList);

            var unitOfWorkMock = new Mock<IOrderUnitOfWork>();

            unitOfWorkMock.Setup(obj => obj.ProductServiceFormatRepository)
                .Returns(formatRepositoryStab.Object);
            unitOfWorkMock.Setup(obj => obj.ProductServiceMaterialRepository)
                .Returns(materialRepositoryStab.Object);

            ProductServiceService service = new ProductServiceService(unitOfWorkMock.Object, mapper);

            // Act

            var expectedResult = await service.GetDataForCreation();

            //Assert

            var expectedMaterialList = expectedResult.Item1.ToList();
            var expectedFormatList = expectedResult.Item2.ToList();

            Assert.Equal(materialList[0].MaterialName, expectedMaterialList[0]);
            Assert.Equal(materialList[1].MaterialName, expectedMaterialList[1]);

            Assert.Equal(formatList[0].FormatName, expectedFormatList[0]);
            Assert.Equal(formatList[1].FormatName, expectedFormatList[1]);
        }
    }
}