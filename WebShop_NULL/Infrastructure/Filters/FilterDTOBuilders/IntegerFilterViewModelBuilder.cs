using WebShop_FSharp.ViewModels.CatalogModels;
using WebShop_NULL.Infrastructure.Filters.FilterDTOs;
using WebShop_NULL.Models.ViewModels;
using WebShop_NULL.Models.ViewModels.FilterViewModels;

namespace WebShop_NULL.Infrastructure.Filters.FilterDTOBuilders
{
    public class IntegerFilterViewModelBuilder : IFilterViewModelBuilder<IntegerFilterViewModel>
    {
        public IntegerFilterViewModel BuildFilterViewModel(FilterViewModel filterViewModel, dynamic filterInfo, dynamic constraints, FilterDTO filterDto = null)
        {
            var model = (IntegerFilterViewModel) filterViewModel;
            model.MinConstraint = constraints["minValue"] ?? long.MinValue;
            model.MaxConstraint = constraints["maxValue"] ?? long.MaxValue;
            if (filterDto != null && filterDto is IntegerFilterDto dto)
            {
                model.Min = dto.Min;
                model.Max = dto.Max;
            }
            return model;
        }
    }
}