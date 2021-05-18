using WebShop_FSharp.ViewModels.CatalogModels;
using WebShop_NULL.Infrastructure.Filters.FilterDTOs;
using WebShop_NULL.Models.ViewModels;
using WebShop_NULL.Models.ViewModels.FilterViewModels;

namespace WebShop_NULL.Infrastructure.Filters.FilterDTOBuilders
{
    public class DecimalFilterViewModelBuilder : IFilterViewModelBuilder<DecimalFilterViewModel>
    {
        public DecimalFilterViewModel BuildFilterViewModel(FilterViewModel filterViewModel, dynamic filterInfo, dynamic constraints, FilterDTO filterDto = null)
        {
            var model = (DecimalFilterViewModel) filterViewModel;
            model.Min = constraints["minValue"] ?? long.MinValue;
            model.Max = constraints["maxValue"] ?? long.MaxValue;
            if (filterDto != null && filterDto is DecimalFilterDto dto)
            {
                model.Min = dto.Min;
                model.Max = dto.Max;
            }
            return model;
        }
    }
}