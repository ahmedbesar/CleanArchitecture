using CleanArchitecture.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CleanArchitecture.Application.Common.Models
{
    public sealed record OrderByOption<T>(
       Expression<Func<T, object>> KeySelector,
       SortDirectionEnum Direction
   );
}
