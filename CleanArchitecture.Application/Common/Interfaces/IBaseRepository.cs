using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Common;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        // Queries
        Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<T?> GetByIdWithIncludesAsync(
            Guid id,
            Expression<Func<T, object>>[] includes,
            CancellationToken cancellationToken = default);

        IQueryable<T> Get(QueryOptions<T>? options = null);
        Task<int> CountAsync(
          Expression<Func<T, bool>>? filter = null,
          CancellationToken cancellationToken = default);

        // Commands
        Task AddAsync(
            T entity,
            CancellationToken cancellationToken = default);

        void Update(T entity);
        void Delete(Guid id);
        void Recover(Guid id);
    }
}
