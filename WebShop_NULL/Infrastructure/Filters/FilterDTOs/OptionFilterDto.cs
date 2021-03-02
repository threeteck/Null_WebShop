using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebShop_NULL.Models.DomainModels;

namespace WebShop_NULL.Infrastructure.Filters.FilterDTOs
{
    [ForPropertyType((int)PropertyTypeEnum.Option)]
    public class OptionFilterDto : FilterDTO
    {
        public List<string> Options { get; set; } = new List<string>();
        protected override Expression<Func<Product, bool>> GenerateExpression()
        {
            Expression<Func<Product, bool>> expr = null;
            if (Options.Count > 0)
            {
                var option = Options[0];
                expr = p =>
                    p.AttributeValues.RootElement.GetProperty(PropertyId.ToString())
                        .GetString() == option;
            }

            foreach (var option in Options.Skip(1))
                expr = expr.Or(p => p.AttributeValues.RootElement.GetProperty(PropertyId.ToString())
                    .GetString() == option);

            return expr;
        }
    }
}