using System;
using System.Linq.Expressions;
using DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace WebShop_NULL.Infrastructure.Filters
{
    [ModelBinder(BinderType = typeof(FilterModelBinder))]
    public abstract class FilterDTO
    {
        protected abstract Expression<Func<Product, bool>> GenerateExpression();
        private Expression<Func<Product, bool>> _expression;

        public Expression<Func<Product, bool>> Expression
        {
            get
            {
                if (_expression == null)
                    _expression = GenerateExpression();
                return _expression;
            }
        }
        
        public int PropertyId { get; set; }
    }
}