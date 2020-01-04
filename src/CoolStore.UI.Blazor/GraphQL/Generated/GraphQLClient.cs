using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StrawberryShake;

namespace CoolStore.UI.Blazor
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public class GraphQLClient
        : IGraphQLClient
    {
        private const string _clientName = "GraphQLClient";

        private readonly IOperationExecutor _executor;

        public GraphQLClient(IOperationExecutorPool executorPool)
        {
            _executor = executorPool.CreateExecutor(_clientName);
        }

        public Task<IOperationResult<IGetProducts>> GetProductsAsync(
            Optional<int> currentPage = default,
            Optional<double> highPrice = default,
            CancellationToken cancellationToken = default)
        {

            return _executor.ExecuteAsync(
                new GetProductsOperation
                {
                    CurrentPage = currentPage, 
                    HighPrice = highPrice
                },
                cancellationToken);
        }

        public Task<IOperationResult<IGetProducts>> GetProductsAsync(
            GetProductsOperation operation,
            CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _executor.ExecuteAsync(operation, cancellationToken);
        }

        public Task<IOperationResult<IGetInventories>> GetInventoriesAsync(
            CancellationToken cancellationToken = default)
        {

            return _executor.ExecuteAsync(
                new GetInventoriesOperation(),
                cancellationToken);
        }

        public Task<IOperationResult<IGetInventories>> GetInventoriesAsync(
            GetInventoriesOperation operation,
            CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _executor.ExecuteAsync(operation, cancellationToken);
        }
    }
}
