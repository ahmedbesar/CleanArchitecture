using CleanArchitecture.Application.Common.Enums;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CleanArchitecture.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<T> DbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        // ================= Queries =================

        public async Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ID == id && !x.IsDeleted, cancellationToken);
        }

        public async Task<T?> GetByIdWithIncludesAsync(
            Guid id,
            Expression<Func<T, object>>[] includes,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = DbSet.AsNoTracking();

            foreach (var include in includes)
                query = query.Include(include);

            return await query
                .FirstOrDefaultAsync(x => x.ID == id && !x.IsDeleted, cancellationToken);
        }

        public IQueryable<T> Get(QueryOptions<T>? options = null)
        {
            IQueryable<T> query = DbSet.AsNoTracking().Where(x => !x.IsDeleted);

            if (options == null)
                return query;

            // Filters
            foreach (var filter in options.Filters)
                query = query.Where(filter);

            // Includes
            foreach (var include in options.Includes)
                query = query.Include(include);

            // Ordering
            IOrderedQueryable<T>? orderedQuery = null;
            foreach (var order in options.OrderBy)
            {
                if (orderedQuery is null)
                {
                    orderedQuery = order.Direction == SortDirectionEnum.Ascending
                        ? query.OrderBy(order.KeySelector)
                        : query.OrderByDescending(order.KeySelector);
                }
                else
                {
                    orderedQuery = order.Direction == SortDirectionEnum.Ascending
                        ? orderedQuery.ThenBy(order.KeySelector)
                        : orderedQuery.ThenByDescending(order.KeySelector);
                }
            }

            if (orderedQuery is not null)
                query = orderedQuery;

            // Pagination
            if (options.Skip.HasValue)
                query = query.Skip(options.Skip.Value);

            if (options.Take.HasValue)
                query = query.Take(options.Take.Value);

            return query;
        }

        public async Task<int> CountAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = DbSet.AsNoTracking().Where(x => !x.IsDeleted);

            if (filter != null)
                query = query.Where(filter);

            return await query.CountAsync(cancellationToken);
        }

        // ================= Commands =================

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
        }

        public void Update(T entity)
        {
            entity.MarkUpdated(Guid.Empty);
            DbSet.Update(entity);
        }

        public void Delete(Guid id)
        {
            var entity = DbSet.First(x => x.ID == id);
            entity.Delete(Guid.Empty);
            DbSet.Update(entity);
        }

        public void Recover(Guid id)
        {
            var entity = DbSet.First(x => x.ID == id);
            entity.Recover(Guid.Empty);
            DbSet.Update(entity);
        }
    }
}
