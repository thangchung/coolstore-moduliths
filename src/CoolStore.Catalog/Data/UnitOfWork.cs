using CoolStore.Catalog.Domain;
using MediatR;
using Moduliths.Domain;
using Moduliths.Infra;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoolStore.Catalog.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogDbContext _dbContext;
        private readonly IMediator _mediator;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public UnitOfWork(CatalogDbContext rentalContext, IMediator mediator)
        {
            _dbContext = rentalContext;
            _mediator = mediator;
        }
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            int count = await _dbContext.SaveChangesAsync(cancellationToken);
            await _mediator.DispatchDomainEventsAsync<Guid, ProductId>(_dbContext);
            return count;
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
