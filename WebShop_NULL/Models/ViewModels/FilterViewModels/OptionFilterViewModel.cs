using System.Collections.Generic;
using WebShop_NULL.Infrastructure.Filters;
using WebShop_NULL.Models.DomainModels;

namespace WebShop_NULL.Models.ViewModels.FilterViewModels
{
    [ForPropertyType((int)PropertyTypeEnum.Option)]
    public class OptionFilterViewModel : FilterViewModel
    {
        public IEnumerable<string> Options { get; set; }
    }
}