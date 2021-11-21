using Business.Interface.Services;
using Data.Interface.UnitOfWork;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Data;

namespace Business.Services
{
    public class ProfileRegistrationService : IProfileRegistrationService
    {
        private readonly UserManager<SystemUser> _userManager;
        private IOrderUnitOfWork _unitOfWork;
        public ProfileRegistrationService(UserManager<SystemUser> userManager,
            IOrderUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<bool> CreateProfile(SystemUser user, string firstName, string lastName)
        {
            IList<string> role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Customer"))
            {
                await _unitOfWork.CustomerRepository.Add(new Customer(Guid.Parse(user.Id))
                {
                    Name = firstName,
                    Surname = lastName
                });
                await _unitOfWork.Save();
                return true;
            }
            else if (role.Contains("Employee"))
            {
                await _unitOfWork.EmployeeRepository.Add(new Employee(Guid.Parse(user.Id))
                {
                    Name = firstName,
                    Surname = lastName
                });
                await _unitOfWork.Save();
                return true;
            }
            else if (role.Contains("Admin"))
            {
                await _unitOfWork.AdminRepository.Add(new Admin(Guid.Parse(user.Id))
                {
                    Name = firstName,
                    Surname = lastName
                });
                await _unitOfWork.Save();
                return true;
            }
            else
            {
                throw new Exception("Role is not set");
            }
        }
    }
}
