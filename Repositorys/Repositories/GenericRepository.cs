

using Entity;
using Microsoft.EntityFrameworkCore;
using Repositorys.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Repositories.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Base
    {
        private readonly S3InnovationDbContext context;

        public GenericRepository(S3InnovationDbContext context) => this.context = context;
        
        public void Create(TEntity entity, string createdBy = null)
        {
            context.Set<TEntity>().Add(entity);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            return GetQueryable(filter, orderBy, includeProperties, skip, take).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            return await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }

        public TEntity GetById(object id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public Task<TEntity> GetByIdAsync(object id)
        {
            return context.Set<TEntity>().FindAsync(id);
        }

        public int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task SaveAsync()
        {
            try
            {
                return context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(TEntity entity, string modifiedBy = null)
        {
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        private IQueryable<TEntity> GetQueryable(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = null,
           int? skip = null,
           int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }
    }
}
