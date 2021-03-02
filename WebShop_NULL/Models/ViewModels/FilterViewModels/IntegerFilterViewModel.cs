using WebShop_NULL.Infrastructure.Filters;
using WebShop_NULL.Models.DomainModels;

namespace WebShop_NULL.Models.ViewModels.FilterViewModels
{
    [ForPropertyType((int)PropertyTypeEnum.Integer)]
    public class IntegerFilterViewModel : FilterViewModel
    {
        public int Min { get; set; } = 0;
        public int Max { get; set; } = 0;
    }
}