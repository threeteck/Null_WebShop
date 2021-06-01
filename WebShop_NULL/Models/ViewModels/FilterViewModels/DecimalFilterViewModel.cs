using DomainModels;
using WebShop_FSharp.ViewModels.CatalogModels;
using WebShop_NULL.Infrastructure.Filters;

namespace WebShop_NULL.Models.ViewModels.FilterViewModels
{
    [ForPropertyType(PropertyTypeEnum.Decimal)]
    public class DecimalFilterViewModel : FilterViewModel
    {
        public decimal Min { get; set; } = long.MinValue;
        public decimal Max { get; set; } = long.MaxValue;

        public decimal MinConstraint { get; set; } = long.MinValue;
        public decimal MaxConstraint { get; set; } = long.MaxValue;
    }
}