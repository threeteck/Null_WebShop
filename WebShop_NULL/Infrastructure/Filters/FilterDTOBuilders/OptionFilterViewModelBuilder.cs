using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using WebShop_FSharp.ViewModels.CatalogModels;
using WebShop_NULL.Infrastructure.Filters.FilterDTOs;
using WebShop_NULL.Models.ViewModels;
using WebShop_NULL.Models.ViewModels.FilterViewModels;

namespace WebShop_NULL.Infrastructure.Filters.FilterDTOBuilders
{
    public class OptionFilterViewModelBuilder : IFilterViewModelBuilder<OptionFilterViewModel>
    {
        public OptionFilterViewModel BuildFilterViewModel(FilterViewModel filterViewModel, dynamic filterInfo, dynamic constraints, FilterDTO filterDto = null)
        {
            var model = (OptionFilterViewModel) filterViewModel;
            JArray optionsJson = filterInfo["options"];
            model.Options = optionsJson.ToObject<List<string>>();
            if (filterDto != null && filterDto is OptionFilterDto dto)
            {
                model.ChosenOptions = dto.Options.ToHashSet();
            }
            return model;
        }
    }
}