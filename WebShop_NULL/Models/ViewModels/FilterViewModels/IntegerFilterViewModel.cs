using DomainModels;
using WebShop_FSharp.ViewModels.CatalogModels;
using WebShop_NULL.Infrastructure.Filters;

namespace WebShop_NULL.Models.ViewModels.FilterViewModels
{
    [ForPropertyType(PropertyTypeEnum.Integer)]
    public class IntegerFilterViewModel : FilterViewModel
    {
        public long Min { get; set; } = 0;
        public long Max { get; set; } = 0;
        
        public long MinConstraint { get; set; } = long.MinValue;
        public long MaxConstraint { get; set; } = long.MaxValue;
    }
}