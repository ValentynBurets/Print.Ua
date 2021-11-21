using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity.Base;

namespace Domain.Repositories.Base
{
    public interface IEntityRepository<TEntity>
        where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetById(Guid id);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
    }
}
