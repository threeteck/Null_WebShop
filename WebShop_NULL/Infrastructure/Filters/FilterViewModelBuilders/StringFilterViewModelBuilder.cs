using WebShop_FSharp.ViewModels.CatalogModels;
using WebShop_NULL.Infrastructure.Filters.FilterDTOs;
using WebShop_NULL.Models.ViewModels;
using WebShop_NULL.Models.ViewModels.FilterViewModels;

namespace WebShop_NULL.Infrastructure.Filters.FilterDTOBuilders
{
    public class StringFilterViewModelBuilder : IFilterViewModelBuilder<StringFilterViewModel>
    {
        public StringFilterViewModel BuildFilterViewModel(FilterViewModel filterViewModel, dynamic filterInfo, dynamic constraints, FilterDTO filterDto = null)
        {
            var model = (StringFilterViewModel) filterViewModel;
            if (filterDto != null && filterDto is StringFilterDto dto)
                model.Query = dto.Query;
            return model;
        }
    }
}