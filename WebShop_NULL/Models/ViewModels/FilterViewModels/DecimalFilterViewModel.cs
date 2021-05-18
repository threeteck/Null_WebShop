using DomainModels;
using WebShop_FSharp.ViewModels.CatalogModels;
using WebShop_NULL.Infrastructure.Filters;

namespace WebShop_NULL.Models.ViewModels.FilterViewModels
{
    [ForPropertyType(PropertyTypeEnum.Decimal)]
    public class DecimalFilterViewModel : FilterViewModel
    {
        public decimal Min { get; set; } = 0;
        public decimal Max { get; set; } = 0;
    }
}