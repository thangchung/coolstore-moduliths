using MediatR;
using Microsoft.EntityFrameworkCore;
using Moduliths.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Moduliths.Infra
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync<TId, TIdentityBase>(this IMediator mediator, DbContext ctx) where TIdentityBase : IdentityBase<TId>
        {
            var domainEntities = ctx.ChangeTracker.Entries<EntityBase<TId, TIdentityBase>>().Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());
            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();
            domainEntities.ToList().ForEach(entity => entity.Entity.DomainEvents.Clear());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
