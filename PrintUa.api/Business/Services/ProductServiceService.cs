using AutoMapper;
using Business.Contract.Model;
using Business.Contract.Services.Entity;
using Business.Model;
using Data.Interface.UnitOfWork;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Entity
{
    public class ProductServiceService : IProductServiceService
    {
        private readonly IOrderUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ProductServiceService(IOrderUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(CreateServiceViewModel createService)
        {
            if(await _unitOfWork.ProductServiceRepository.GetByName(createService.Name) != null)
            {
                throw new ValidationException("Service with this name exists");
            }

            var existingMaterial = await _unitOfWork.ProductServiceMaterialRepository.GetByMaterialName(createService.MaterialName);
            
            if(existingMaterial == null)
            {
                await _unitOfWork.ProductServiceMaterialRepository.Add(new ServiceMaterial() { MaterialName = createService.MaterialName });
                await _unitOfWork.Save();
                existingMaterial = await _unitOfWork.ProductServiceMaterialRepository.GetByMaterialName(createService.MaterialName);
            }

            var existingFormat = await _unitOfWork.ProductServiceFormatRepository.GetByFormatName(createService.FormatName);

            if (existingFormat == null)
            {
                await _unitOfWork.ProductServiceFormatRepository.Add(new ServiceFormat() { FormatName = createService.FormatName });
                await _unitOfWork.Save();
                existingFormat = await _unitOfWork.ProductServiceFormatRepository.GetByFormatName(createService.FormatName);
            }

            var newService = _mapper.Map<CreateServiceViewModel, ProductService>(createService);
            
            newService.MaterialId = existingMaterial.Id;
            newService.FormatId = existingFormat.Id;

            var existingService = await _unitOfWork.ProductServiceRepository.GetByMaterialAndFormat(existingMaterial.Id, existingFormat.Id);
            
            if (existingService != null)
            {
                throw new ValidationException("Service with these format and material exists");
            }

            await _unitOfWork.ProductServiceRepository.Add(newService);
            await _unitOfWork.Save();
        }

        public async Task DisableService(Guid serviceId)
        {
            await ChageServiceState(serviceId, true);
        }

        public async Task EnableService(Guid serviceId)
        {
            await ChageServiceState(serviceId, false);
        }

        private async Task ChageServiceState(Guid serviceId, bool flag)
        {
            var service = await _unitOfWork.ProductServiceRepository.GetById(serviceId);

            if (service == null)
            {
                throw new ValidationException($"Can't find sevice with Id ({serviceId}). Operation canceled");
            }

            service.IsCanceled = flag;
            await _unitOfWork.ProductServiceRepository.Update(service);
            await _unitOfWork.Save();
        }

        public async Task Delete(Guid serviceId)
        {
            var service = await _unitOfWork.ProductServiceRepository.GetById(serviceId);

            if (service == null)
            {
                throw new ValidationException($"Can't find sevice with Id ({serviceId}). Operation canceled");
            }

            await _unitOfWork.ProductServiceRepository.Remove(service);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<ServiceViewModel>> GetAll()
        {
            var services = await _unitOfWork.ProductServiceRepository.GetAll();

            var servicesInfoCollection = new List<ServiceViewModel>();  
                           
            foreach(ProductService item in services)
            {
                var serviceInfo = _mapper.Map<ProductService, ServiceViewModel>(item);
                var material = await _unitOfWork.ProductServiceMaterialRepository.GetById(item.MaterialId);
                var format = await _unitOfWork.ProductServiceFormatRepository.GetById(item.FormatId);

                serviceInfo.Material = material;
                serviceInfo.Format = format;

                servicesInfoCollection.Add(serviceInfo);
            }

            return servicesInfoCollection;
        }

        public async Task Update(Guid serviceId, UpdateServiceViewModel updateService)
        {
            var existService = await _unitOfWork.ProductServiceRepository.GetById(serviceId);

            if (existService == null) throw new ValidationException($"Can't find sevice with Id ({serviceId}). Operation canceled");

            if (existService.Material.MaterialName != updateService.MaterialName)
            {
                var updateMaterial = await _unitOfWork.ProductServiceMaterialRepository.GetByMaterialName(updateService.MaterialName);
                if(updateMaterial == null)
                {
                    await _unitOfWork.ProductServiceMaterialRepository.Add(new ServiceMaterial() { MaterialName = updateService.MaterialName });
                    await _unitOfWork.Save();

                    updateMaterial = await _unitOfWork.ProductServiceMaterialRepository.GetByMaterialName(updateService.MaterialName);
                }
                existService.MaterialId = updateMaterial.Id;
            }


            if (existService.Format.FormatName != updateService.FormatName)
            {
                var updateFormat = await _unitOfWork.ProductServiceFormatRepository.GetByFormatName(updateService.FormatName);
                if (updateFormat == null)
                {
                    await _unitOfWork.ProductServiceFormatRepository.Add(new ServiceFormat() { FormatName = updateService.FormatName });
                    await _unitOfWork.Save();

                    updateFormat = await _unitOfWork.ProductServiceFormatRepository.GetByFormatName(updateService.FormatName);
                }
                existService.FormatId = updateFormat.Id;
            }

            var existingService = await _unitOfWork.ProductServiceRepository.GetByMaterialAndFormat(existService.MaterialId, existService.FormatId);

            if (existingService != null && existingService.Id != serviceId)
            {
                throw new ValidationException("Service with these format and material exists");
            }

            var serviceByName = await _unitOfWork.ProductServiceRepository.GetByName(updateService.Name);

            if (serviceByName != null && serviceByName.Id != serviceId)
            {
                throw new ValidationException("Service with this name exists");
            }

            existService.Cost = updateService.Cost;
            existService.Name = updateService.Name;
            existService.IsCanceled = updateService.IsCanceled;

            await _unitOfWork.ProductServiceRepository.Update(existService);
            await _unitOfWork.Save();
        }

        public async Task<(IEnumerable<string>, IEnumerable<string>)> GetDataForCreation()
        {
            var materials = (await _unitOfWork.ProductServiceMaterialRepository.GetAll()).Select(m => m.MaterialName).ToList();
            var formats = (await _unitOfWork.ProductServiceFormatRepository.GetAll()).Select(m => m.FormatName).ToList();
            return (materials, formats);
        }

        public async Task<ServiceViewModel> GetById(Guid serviceId)
        {
            var services = await _unitOfWork.ProductServiceRepository.GetById(serviceId);
            var serviceInfo = _mapper.Map<ProductService, ServiceViewModel>(services);
            var material = await _unitOfWork.ProductServiceMaterialRepository.GetById(services.MaterialId);
            var format = await _unitOfWork.ProductServiceFormatRepository.GetById(services.FormatId);
            serviceInfo.Material = material;
            serviceInfo.Format = format;
            return serviceInfo;
        }

        public async Task<IEnumerable<ServiceViewModel>> GetAllNotCanceled()
        {
            var services = await _unitOfWork.ProductServiceRepository.GetAllNotCanceled();

            var servicesInfoCollection = new List<ServiceViewModel>();

            foreach (ProductService item in services)
            {
                var serviceInfo = _mapper.Map<ProductService, ServiceViewModel>(item);
                var material = await _unitOfWork.ProductServiceMaterialRepository.GetById(item.MaterialId);
                var format = await _unitOfWork.ProductServiceFormatRepository.GetById(item.FormatId);

                serviceInfo.Material = material;
                serviceInfo.Format = format;

                servicesInfoCollection.Add(serviceInfo);
            }

            return servicesInfoCollection;
        }
    }
}
