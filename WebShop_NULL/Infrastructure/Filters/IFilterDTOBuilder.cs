using WebShop_FSharp.ViewModels.CatalogModels;
using WebShop_NULL.Models.ViewModels;

namespace WebShop_NULL.Infrastructure.Filters
{
    public interface IFilterViewModelBuilder<out T>
    {
        T BuildFilterViewModel(FilterViewModel filterViewModel, dynamic filterInfo, dynamic constraints, FilterDTO filterDto = null);
    }
}