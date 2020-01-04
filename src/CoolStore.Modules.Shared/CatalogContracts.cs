using MediatR;
using Moduliths.Domain;
using System;
using static Moduliths.Infra.Helpers.DateTimeHelper;

namespace CoolStore.Protobuf.Catalogs.V1
{
    public partial class GetProductsRequest : IRequest<GetProductsResponse> { }
    public partial class GetProductByIdRequest : IRequest<GetProductByIdResponse> { }
    public partial class CreateProductRequest : IRequest<CreateProductResponse> { }
    public partial class UpdateProductRequest : IRequest<UpdateProductResponse> { }
    public partial class DeleteProductRequest : IRequest<DeleteProductResponse> { }

    public partial class ProductUpdated : IDomainEvent
    {
        public DateTime CreatedAt => NewDateTime();
    }

    public partial class ProductDeleted : IDomainEvent
    {
        public DateTime CreatedAt => NewDateTime();
    }
}
