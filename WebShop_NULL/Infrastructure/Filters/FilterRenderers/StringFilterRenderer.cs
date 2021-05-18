using DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebShop_FSharp.ViewModels.CatalogModels;
using WebShop_NULL.Infrastructure.Filters.FilterDTOs;
using WebShop_NULL.Models.ViewModels;
using WebShop_NULL.Models.ViewModels.FilterViewModels;

namespace WebShop_NULL.Infrastructure.Filters.FilterRenderers
{
    [ForPropertyType(PropertyTypeEnum.Nominal)]
    public class StringFilterRenderer : IFilterRenderer<StringFilterViewModel>
    {
        public IViewComponentResult Render(StringFilterViewModel model)
        {
            return new ViewViewComponentResult()
            {
                ViewName = "StringFilterPartial"
            };
        }

        public IViewComponentResult Render(FilterViewModel model)
        {
            return Render((StringFilterViewModel) model);
        }
    }
}