using AutoMapper;
using Business.Interface.Models;
using Business.Interface.Services;
using Data.Interface.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UserIdentity.Data;
using System.Linq;
using Domain.Entity;

namespace Business.Services
{
    public class ProfileDataService : IProfileDataService
    {
        private readonly IOrderUnitOfWork _unitOfWork;
        private readonly IProfileManager _profileManager;
        private readonly IMapper _mapper;

        public ProfileDataService(IOrderUnitOfWork unitOfWork, IMapper mapper, IProfileManager profileManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _profileManager = profileManager;
    }

        public async Task<ProfileInfoModel> GetCustomerProfileInfoById(Guid id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdLink(id);

            if (customer == null)
            {
                throw new Exception("Customer with this id was not found!");
            }

            var profileInfo = _mapper.Map<Customer, ProfileInfoModel>(customer);

            return profileInfo;
        }

        public async Task<ProfileInfoModel> GetEmployeeProfileInfoById(Guid id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdLink(id);

            if (employee == null)
            {
                throw new Exception("Employee with this id was not found!");
            }

            var profileInfo = _mapper.Map<Employee, ProfileInfoModel>(employee);

            return profileInfo;
        }

        public async Task<ProfileInfoModel> GetAdminProfileInfoById(Guid id)
        {
            var admin = await _unitOfWork.AdminRepository.GetByIdLink(id);

            if (admin == null)
            {
                throw new Exception("Admin with this id was not found!");
            }

            var profileInfo = _mapper.Map<Admin, ProfileInfoModel>(admin);

            return profileInfo;
        }

        public async Task UpdateCustomerProfileInfoById(ProfileInfoModel model, Guid id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdLink(id);

            if (customer == null)
            {
                throw new Exception("Customer with this id was not found!");
            }

            customer.Name = model.Name;
            customer.Surname = model.Surname;

            await _unitOfWork.Save();
        }

        public async Task UpdateEmployeeProfileInfoById(ProfileInfoModel model, Guid id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdLink(id);

            if (employee == null)
            {
                throw new Exception("Employee with this id was not found!");
            }

            employee.Name = model.Name;
            employee.Surname = model.Surname;

            await _unitOfWork.Save();
        }

        public async Task UpdateAdminProfileInfoById(ProfileInfoModel model, Guid id)
        {
            var admin = await _unitOfWork.AdminRepository.GetByIdLink(id);

            if (admin == null)
            {
                throw new Exception("Admin with this id was not found!");
            }

            admin.Name = model.Name;
            admin.Surname = model.Surname;

            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<UserInfoViewModel>> GetAllUsersInfo()
        {
            List<UserInfoViewModel> userList = new List<UserInfoViewModel>();

            var customers = (await _unitOfWork.CustomerRepository.GetAll()).ToList();

            if (customers == null)
            {
                throw new Exception("Customers with was not found!");
            }

            foreach(var item in customers)
            {
                var email = await _profileManager.GetEmailByUserId(item.IdLink);
                var phoneNumber = await _profileManager.GetPhoneNumberByUserId(item.IdLink);
                var user = _mapper.Map<Customer, UserInfoViewModel>(item);
                user.Email = email;
                user.PhoneNumber = phoneNumber;
                userList.Add(user);
            }

            var admins = (await _unitOfWork.AdminRepository.GetAll()).ToList();

            if (admins == null)
            {
                throw new Exception("admins with was not found!");
            }

            foreach (var item in admins)
            {
                var email = await _profileManager.GetEmailByUserId(item.IdLink);
                var phoneNumber = await _profileManager.GetPhoneNumberByUserId(item.IdLink);
                var user = _mapper.Map<Admin, UserInfoViewModel>(item);
                user.Email = email;
                user.PhoneNumber = phoneNumber;
                userList.Add(user);
            }

            var employees = (await _unitOfWork.EmployeeRepository.GetAll()).ToList();

            if (employees == null)
            {
                throw new Exception("employees with was not found!");
            }

            foreach (var item in employees)
            {
                var email = await _profileManager.GetEmailByUserId(item.IdLink);
                var phoneNumber = await _profileManager.GetPhoneNumberByUserId(item.IdLink);
                var user = _mapper.Map<Employee, UserInfoViewModel>(item);
                user.Email = email;
                user.PhoneNumber = phoneNumber;
                userList.Add(user);
            }

            return userList;
        }
    }
}
