using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Repositories.Base;

namespace Domain.Repository.UserManagement
{
    public interface IEmployeeRepository : IUserRepository<Employee>
    {
    }
}
