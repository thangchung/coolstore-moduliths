using CoolStore.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Moduliths.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolStore.Catalog.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _dbContext;

        public ProductRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext ?? throw CoreException.NullArgument(nameof(dbContext));
        }

        public void Add(Product entity)
        {
            _dbContext.Products.Add(entity);
        }

        public async Task<IEnumerable<Product>> FindAllAsync(ISpecification<Product> specification)
        {
            // TODO fix it
            //var query = specification.Includes
            //    .Aggregate(_dbContext.Set<Product>().AsQueryable(), (current, include) => current.Include(include));
            //return await query.Where(specification.Expression).AsNoTracking().ToListAsync();

            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> FindOneAsync(ISpecification<Product> specification)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(specification.Expression);
        }

        public void Remove(Product entity)
        {
            _dbContext.Products.Remove(entity);
        }

        public void Update(Product entity)
        {
            _dbContext.Products.Update(entity);
        }
    }
}
