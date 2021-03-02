using WebShop_NULL.Models.ViewModels;
using WebShop_NULL.Models.ViewModels.FilterViewModels;

namespace WebShop_NULL.Infrastructure.Filters.FilterViewModelBuilders
{
    public class IntegerFilterViewModelBuilder : IFilterViewModelBuilder<IntegerFilterViewModel>
    {
        public IntegerFilterViewModel BuildFilterViewModel(FilterViewModel filterViewModel, dynamic filterInfo)
        {
            var model = (IntegerFilterViewModel) filterViewModel;
            model.Min = filterInfo["min"];
            model.Max = filterInfo["max"];
            return model;
        }
    }
}