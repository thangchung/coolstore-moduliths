using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moduliths.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        void Rollback();
    }
}
