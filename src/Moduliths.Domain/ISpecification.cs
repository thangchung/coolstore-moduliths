using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Moduliths.Domain
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Expression { get; }
        List<Expression<Func<T, object>>> Includes { get; }
    }
}
