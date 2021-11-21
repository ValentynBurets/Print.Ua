using Business.Contract.Model;
using Business.Model;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contract.Services.Entity
{
    public interface IProductServiceService
    {
		Task Create(CreateServiceViewModel createService);
		Task<IEnumerable<ServiceViewModel>> GetAll();
		Task<IEnumerable<ServiceViewModel>> GetAllNotCanceled();
		Task DisableService(Guid serviceId);
		Task EnableService(Guid serviceId);
		Task<ServiceViewModel> GetById(Guid serviceId);
		Task Update(Guid serviceId, UpdateServiceViewModel service);
		Task Delete(Guid serviceId);
		Task<(IEnumerable<string>, IEnumerable<string>)> GetDataForCreation();
	}
}
