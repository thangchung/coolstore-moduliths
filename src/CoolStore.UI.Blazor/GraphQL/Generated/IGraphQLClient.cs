using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StrawberryShake;

namespace CoolStore.UI.Blazor
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public interface IGraphQLClient
    {
        Task<IOperationResult<IGetProducts>> GetProductsAsync(
            Optional<int> currentPage = default,
            Optional<double> highPrice = default,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<IGetProducts>> GetProductsAsync(
            GetProductsOperation operation,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<IGetInventories>> GetInventoriesAsync(
            CancellationToken cancellationToken = default);

        Task<IOperationResult<IGetInventories>> GetInventoriesAsync(
            GetInventoriesOperation operation,
            CancellationToken cancellationToken = default);
    }
}
