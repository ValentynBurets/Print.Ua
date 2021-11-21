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
    public class AdminRepository : EntityRepositoryClass<Admin>, IAdminRepository
    {
        public AdminRepository(DomainDbContext orderDbContext) : base(orderDbContext)
        {
        }

        public async Task<Admin> GetByIdLink(Guid idLink)
        {
            return await _DbContext.Admins.FirstAsync(e => e.IdLink == idLink);
        }
        public async Task<Admin> GetByIdLinkOrDefault(Guid idLink)
        {
            return await _DbContext.Admins.FirstOrDefaultAsync(e => e.IdLink == idLink);
        }
        public async Task<IEnumerable<Admin>> GetByName(string name)
        {
            return await _DbContext.Admins.Where(e => e.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<Admin>> GetBySurname(string surname)
        {
            return await _DbContext.Admins.Where(e => e.Surname == surname).ToListAsync();
        }
    }
}
