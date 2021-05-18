using System.Collections.Generic;
using DomainModels;
using WebShop_FSharp.ViewModels.CatalogModels;
using WebShop_NULL.Infrastructure.Filters;

namespace WebShop_NULL.Models.ViewModels.FilterViewModels
{
    [ForPropertyType(PropertyTypeEnum.Option)]
    public class OptionFilterViewModel : FilterViewModel
    {
        public List<string> Options { get; set; } = new List<string>();
        public HashSet<string> ChosenOptions { get; set; } = new HashSet<string>();
    }
}