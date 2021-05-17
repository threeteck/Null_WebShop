using System.Collections.Generic;
using DomainModels;
using WebShop_NULL.Infrastructure.Filters;

namespace WebShop_NULL.Models.ViewModels.FilterViewModels
{
    [ForPropertyType(PropertyTypeEnum.Option)]
    public class OptionFilterViewModel : FilterViewModel
    {
        public IEnumerable<string> Options { get; set; }
    }
}