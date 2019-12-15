using MediatR;
using System;

namespace Moduliths.Domain
{
    public interface IDomainEvent : INotification
    {
        DateTime CreatedAt { get; }
    }
}
