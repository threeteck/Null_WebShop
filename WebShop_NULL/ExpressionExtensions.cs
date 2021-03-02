using System;
using System.Linq.Expressions;

namespace WebShop_NULL
{
    public static class ExpressionExtensions
    {
        public static Expression<T> Compose<T>(this Expression<T> first,
            Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            throw new NotImplementedException();
        }

        public static Expression<T> Or<T>(this Expression<T> first,
            Expression<T> second)
            => first.Compose(second, Expression.Or);
    }
}