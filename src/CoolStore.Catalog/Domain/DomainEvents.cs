using Moduliths.Domain;
using System;
using static Moduliths.Infra.Helpers.DateTimeHelper;

namespace CoolStore.Protobuf.Catalogs.V1
{
    public partial class ProductUpdated : IDomainEvent
    {
        public DateTime CreatedAt => NewDateTime();
    }

    public partial class ProductDeleted : IDomainEvent
    {
        public DateTime CreatedAt => NewDateTime();
    }
}
