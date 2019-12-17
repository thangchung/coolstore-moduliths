using System;
using System.Collections.Generic;

namespace Moduliths.Domain
{
    public abstract class EntityBase<TId, TIdentityBase> where TIdentityBase : IdentityBase<TId>
    {
        protected IdentityBase<TId> Id;
        protected EntityBase(IdentityBase<TId> id) => Id = id;
        protected EntityBase() { }
        public DateTime Created { get; protected set; }
        public DateTime? Updated { get; protected set; }
        public HashSet<IDomainEvent> DomainEvents { get; private set; }

        public void AddDomainEvent(IDomainEvent eventItem)
        {
            DomainEvents = DomainEvents ?? new HashSet<IDomainEvent>();
            DomainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            DomainEvents?.Remove(eventItem);
        }
    }
}
