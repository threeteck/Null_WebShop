using DomainModels;
using WebShop_FSharp.ViewModels.CatalogModels;
using WebShop_NULL.Infrastructure.Filters;

namespace WebShop_NULL.Models.ViewModels.FilterViewModels
{
    [ForPropertyType(PropertyTypeEnum.Nominal)]
    public class StringFilterViewModel : FilterViewModel
    {
        public string Query { get; set; } = "";
    }
}