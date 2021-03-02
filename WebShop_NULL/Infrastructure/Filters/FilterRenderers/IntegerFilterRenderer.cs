using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebShop_NULL.Models.DomainModels;
using WebShop_NULL.Models.ViewModels;
using WebShop_NULL.Models.ViewModels.FilterViewModels;

namespace WebShop_NULL.Infrastructure.Filters.FilterRenderers
{
    [ForPropertyType((int)PropertyTypeEnum.Integer)]
    public class IntegerFilterRenderer : IFilterRenderer<IntegerFilterViewModel>
    {
        public IViewComponentResult Render(IntegerFilterViewModel model)
        {
            return new ViewViewComponentResult()
            {
                ViewName = "IntegerFilterPartial"
            };
        }

        public IViewComponentResult Render(FilterViewModel model)
        {
            return Render((IntegerFilterViewModel) model);
        }
    }
}