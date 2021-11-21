using Data.EF;
using Data.Repository.Base;
using Domain.Entity;
using Domain.Repository.UserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class EmployeeRepository : EntityRepositoryClass<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DomainDbContext orderDbContext) : base(orderDbContext)
        {
        }

        public async Task<Employee> GetByIdLink(Guid idLink)
        {
            return await _DbContext.Employees.FirstAsync(e => e.IdLink == idLink);
        }
        public async Task<Employee> GetByIdLinkOrDefault(Guid idLink)
        {
            return await _DbContext.Employees.FirstOrDefaultAsync(e => e.IdLink == idLink);
        }
        public async Task<IEnumerable<Employee>> GetByName(string name)
        {
            return await _DbContext.Employees.Where(e => e.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetBySurname(string surname)
        {
            return await _DbContext.Employees.Where(e => e.Surname == surname).ToListAsync();
        }
    }
}
