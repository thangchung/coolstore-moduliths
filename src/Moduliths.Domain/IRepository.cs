using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moduliths.Domain
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task<T> FindOneAsync(ISpecification<T> specification);
        Task<IEnumerable<T>> FindAllAsync(ISpecification<T> specification);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
