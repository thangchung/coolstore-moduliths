using MediatR;
using Microsoft.EntityFrameworkCore;
using Moduliths.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moduliths.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IEnumerable<DbContext> _dbContexts;
        private readonly IMediator _mediator;

        public UnitOfWork(IEnumerable<DbContext> dbContexts, IMediator mediator)
        {
            _dbContexts = dbContexts ?? throw CoreException.NullArgument(nameof(dbContexts));
            _mediator = mediator ?? throw CoreException.NullArgument(nameof(mediator));
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            var count = 0;
            foreach (var dbContext in _dbContexts)
            {
                count += await dbContext.SaveChangesAsync(cancellationToken);
                await _mediator.DispatchDomainEventsAsync<Guid, IdentityBase<Guid>>(dbContext);
            }
            return count;
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            foreach (var dbContext in _dbContexts)
            {
                dbContext?.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
