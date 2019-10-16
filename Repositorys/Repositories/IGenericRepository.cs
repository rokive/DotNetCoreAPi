

using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : Base
    {
        void Create(TEntity entity, string createdBy = null);

        void Update(TEntity entity, string modifiedBy = null);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        TEntity GetById(object id);

        Task<TEntity> GetByIdAsync(object id);

        void Save();

        Task SaveAsync();

        int GetCount(Expression<Func<TEntity, bool>> filter = null);
    }
}
