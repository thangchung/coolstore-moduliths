﻿using Microsoft.EntityFrameworkCore;
using Moduliths.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moduliths.Infra.Data
{
    public class RepositoryBase<T, TDbContext> : IRepository<T>
       where T : class, IAggregateRoot
        where TDbContext : DbContext
    {
        public RepositoryBase(TDbContext dbContext)
        {
            DbContext = dbContext ?? throw CoreException.NullArgument(nameof(dbContext));
        }

        public TDbContext DbContext { get; }

        public void Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().AddRange(entities);
        }

        public virtual System.Collections.Generic.IAsyncEnumerable<T> FindAllAsync(ISpecification<T> specification = null)
        {
            var queryable = DbContext.Set<T>().AsQueryable();
            if (specification == null)
            {
                return queryable
                    .AsNoTracking()
                    .AsAsyncEnumerable();
            }
            else
            {
                return queryable
                    .Where(specification.Expression)
                    .AsNoTracking()
                    .AsAsyncEnumerable();
            }
        }

        public Task<T> FindOneAsync(ISpecification<T> specification)
        {
            return DbContext.Set<T>().FirstOrDefaultAsync(specification.Expression);
        }

        public void Remove(T entity)
        {
            DbContext.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            DbContext.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().UpdateRange(entities);
        }
    }
}
