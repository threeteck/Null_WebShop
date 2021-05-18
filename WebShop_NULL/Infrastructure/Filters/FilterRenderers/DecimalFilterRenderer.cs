using DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebShop_FSharp.ViewModels.CatalogModels;
using WebShop_NULL.Infrastructure.Filters.FilterDTOs;
using WebShop_NULL.Models.ViewModels;
using WebShop_NULL.Models.ViewModels.FilterViewModels;

namespace WebShop_NULL.Infrastructure.Filters.FilterRenderers
{
    [ForPropertyType(PropertyTypeEnum.Decimal)]
    public class DecimalFilterRenderer : IFilterRenderer<DecimalFilterViewModel>
    {
        public IViewComponentResult Render(DecimalFilterViewModel model)
        {
            return new ViewViewComponentResult()
            {
                ViewName = "DecimalFilterPartial"
            };
        }

        public IViewComponentResult Render(FilterViewModel model)
        {
            return Render((DecimalFilterViewModel) model);
        }
    }
}