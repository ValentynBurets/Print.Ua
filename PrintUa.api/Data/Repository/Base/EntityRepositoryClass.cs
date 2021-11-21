using Data.EF;
using Domain.Entity.Base;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository.Base
{
    public abstract class EntityRepositoryClass<TEntity> : IEntityRepository<TEntity>
        where TEntity : EntityBase
    {
        protected EntityRepositoryClass(DomainDbContext orderDbContext)
        {
            _DbContext = orderDbContext;
        }

        protected internal DomainDbContext _DbContext { get; set; }

        public async Task Add(TEntity entity)
        {
            await _DbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> expression)
        {
            return await _DbContext.Set<TEntity>().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _DbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _DbContext.Set<TEntity>().FirstAsync(e => e.Id == id);
        }

        public Task Remove(TEntity entity)
        {
            return Task.FromResult(_DbContext.Set<TEntity>().Remove(entity));
        }

        public Task Update(TEntity entity)
        {
            return Task.FromResult(_DbContext.Set<TEntity>().Update(entity));
        }
    }
}
