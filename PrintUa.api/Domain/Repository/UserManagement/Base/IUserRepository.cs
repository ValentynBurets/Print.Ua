using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Entity.Base;
using Domain.Repositories.Base;

namespace Domain.Repository.UserManagement
{
    public interface IUserRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetByName(string name);
        Task<IEnumerable<TEntity>> GetBySurname(string surname);
        Task<TEntity> GetByIdLink(Guid idLink);
        Task<TEntity> GetByIdLinkOrDefault(Guid idLink);
    }
}
