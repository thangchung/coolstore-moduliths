using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moduliths.Domain
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task<T> FindOneAsync(ISpecification<T> specification);
        IAsyncEnumerable<T> FindAllAsync(ISpecification<T> specification = null);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
