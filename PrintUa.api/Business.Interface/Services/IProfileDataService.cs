using Business.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Data;

namespace Business.Interface.Services
{
    public interface IProfileDataService
    {
        public Task<ProfileInfoModel> GetCustomerProfileInfoById(Guid id);
        public Task<ProfileInfoModel> GetEmployeeProfileInfoById(Guid id);
        public Task<IEnumerable<UserInfoViewModel>> GetAllUsersInfo();
        public Task<ProfileInfoModel> GetAdminProfileInfoById(Guid id);
        public Task UpdateCustomerProfileInfoById(ProfileInfoModel model, Guid id);
        public Task UpdateEmployeeProfileInfoById(ProfileInfoModel model, Guid id);
        public Task UpdateAdminProfileInfoById(ProfileInfoModel model, Guid id);
    }
}
