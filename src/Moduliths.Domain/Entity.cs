using System;
using System.Collections.Generic;

namespace Moduliths.Domain
{
    public abstract class Entity
    {
        public virtual Guid Id { get; private set; }
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
