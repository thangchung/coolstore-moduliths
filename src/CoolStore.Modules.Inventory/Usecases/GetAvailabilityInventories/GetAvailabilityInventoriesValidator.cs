using CoolStore.Protobuf.Inventories.V1;
using FluentValidation;

namespace CoolStore.Modules.Inventory.Usecases.GetAvailabilityInventories
{
    public class GetAvailabilityInventoriesValidator : AbstractValidator<GetInventoriesRequest>
    {
        public GetAvailabilityInventoriesValidator()
        {
        }
    }
}
