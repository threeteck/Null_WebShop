using System;
using System.Linq.Expressions;
using DomainModels;

namespace WebShop_NULL.Infrastructure.Filters.FilterDTOs
{
    [ForPropertyType(PropertyTypeEnum.Integer)]
    public class IntegerFilterDto : FilterDTO
    {
        public long Min { get; set; } = long.MinValue;
        public long Max { get; set; } = long.MaxValue;
        
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