using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WebShop_NULL
{
    public static class ExpressionExtensions
    {
        public static Expression<T> Compose<T>(this Expression<T> first,
            Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters
                .Select((f, i) => new {f, s = second.Parameters[i]})
                .ToDictionary(p => p.s, p => p.f);

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<T> Or<T>(this Expression<T> first,
            Expression<T> second)
            => first.Compose(second, Expression.Or);
    }

    public static class ParameterRebinder
    {
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression secondBody)
        {
            var visitor = new ParameterRebinderVisitor(map);
            return visitor.Visit(secondBody);
        }
    }

    public class ParameterRebinderVisitor : ExpressionVisitor
    {
        private Dictionary<ParameterExpression, ParameterExpression> _map;

        public ParameterRebinderVisitor(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            _map = map;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_map.TryGetValue(node, out var replacement))
                node = replacement;

            return base.VisitParameter(node);
        }
    }
}