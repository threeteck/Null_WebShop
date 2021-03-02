using Newtonsoft.Json.Linq;
using WebShop_NULL.Models.ViewModels;
using WebShop_NULL.Models.ViewModels.FilterViewModels;

namespace WebShop_NULL.Infrastructure.Filters.FilterViewModelBuilders
{
    public class OptionFilterViewModelBuilder : IFilterViewModelBuilder<OptionFilterViewModel>
    {
        public OptionFilterViewModel BuildFilterViewModel(FilterViewModel filterViewModel, dynamic filterInfo)
        {
            var model = (OptionFilterViewModel) filterViewModel;
            JArray optionsJson = filterInfo["options"];
            model.Options = optionsJson.ToObject<string[]>();
            return model;
        }
    }
}