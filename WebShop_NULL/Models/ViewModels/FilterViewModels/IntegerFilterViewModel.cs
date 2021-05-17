using DomainModels;
using WebShop_NULL.Infrastructure.Filters;

namespace WebShop_NULL.Models.ViewModels.FilterViewModels
{
    [ForPropertyType(PropertyTypeEnum.Integer)]
    public class IntegerFilterViewModel : FilterViewModel
    {
        public int Min { get; set; } = 0;
        public int Max { get; set; } = 0;
    }
}