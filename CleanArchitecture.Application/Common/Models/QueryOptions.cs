using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CleanArchitecture.Application.Common.Models
{
    public sealed record QueryOptions<T>
    {
        public IReadOnlyList<Expression<Func<T, bool>>> Filters { get; init; }
            = Array.Empty<Expression<Func<T, bool>>>();

        public IReadOnlyList<OrderByOption<T>> OrderBy { get; init; }
            = Array.Empty<OrderByOption<T>>();

        public int? Skip { get; init; }
        public int? Take { get; init; }

        public IReadOnlyList<Expression<Func<T, object>>> Includes { get; init; }
            = Array.Empty<Expression<Func<T, object>>>();
    }
}
