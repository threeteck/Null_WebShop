using System;
using System.Linq.Expressions;
using DomainModels;

namespace WebShop_NULL.Infrastructure.Filters.FilterDTOs
{
    [ForPropertyType(PropertyTypeEnum.Decimal)]
    public class DecimalFilterDto : FilterDTO
    {
        public decimal Min { get; set; } = Decimal.MinValue;
        public decimal Max { get; set; } = Decimal.MaxValue;
        
        protected override Expression<Func<Product, bool>> GenerateExpression()
        {

            return p => p.AttributeValues.RootElement
                .GetProperty(PropertyId.ToString())
                .GetInt32() >= Min && p.AttributeValues.RootElement
                .GetProperty(PropertyId.ToString())
                .GetInt32() <= Max;
        }
    }
}