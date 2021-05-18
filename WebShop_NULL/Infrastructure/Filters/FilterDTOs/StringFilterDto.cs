using System;
using System.Linq.Expressions;
using DomainModels;

namespace WebShop_NULL.Infrastructure.Filters.FilterDTOs
{
    [ForPropertyType(PropertyTypeEnum.Nominal)]
    public class StringFilterDto : FilterDTO
    {
        public string Query { get; set; } = "";
        
        protected override Expression<Func<Product, bool>> GenerateExpression()
        {
            if (string.IsNullOrWhiteSpace(Query))
                return p => true;

            return p => p.AttributeValues.RootElement
                .GetProperty(PropertyId.ToString())
                .GetString().ToLower().Contains(Query.ToLower());
        }
    }
}