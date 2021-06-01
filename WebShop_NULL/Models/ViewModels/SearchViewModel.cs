using System.Collections.Generic;
using WebShop_FSharp.ViewModels.CatalogModels;
using WebShop_NULL.Infrastructure.Filters;

namespace WebShop_NULL.Models.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<FilterViewModel> Filters;
    }
}