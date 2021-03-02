using System;
using System.Linq.Expressions;
using WebShop_NULL.Models.DomainModels;

namespace WebShop_NULL.Infrastructure.Filters.FilterDTOs
{
    [ForPropertyType((int)PropertyTypeEnum.Integer)]
    public class IntegerFilterDto : FilterDTO
    {
        public int Min { get; set; } = Int32.MinValue;
        public int Max { get; set; } = Int32.MaxValue;
        
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