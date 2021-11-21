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
    public class CustomerRepository : EntityRepositoryClass<Customer>, ICustomerRepository
    {
        public CustomerRepository(DomainDbContext orderDbContext) : base(orderDbContext)
        {
        }

        public async Task<Customer> GetByIdLink(Guid idLink)
        {
            return await _DbContext.Customers.FirstAsync(e => e.IdLink == idLink);
        }
        public async Task<Customer> GetByIdLinkOrDefault(Guid idLink)
        {
            return await _DbContext.Customers.FirstOrDefaultAsync(e => e.IdLink == idLink);
        }
        public async Task<IEnumerable<Customer>> GetByName(string name)
        {
            return await _DbContext.Customers.Where(e => e.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetBySurname(string surname)
        {
            return await _DbContext.Customers.Where(e => e.Surname == surname).ToListAsync();
        }
    }
}
