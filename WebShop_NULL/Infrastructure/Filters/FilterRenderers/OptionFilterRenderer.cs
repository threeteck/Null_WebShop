using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebShop_NULL.Models.DomainModels;
using WebShop_NULL.Models.ViewModels;
using WebShop_NULL.Models.ViewModels.FilterViewModels;

namespace WebShop_NULL.Infrastructure.Filters.FilterRenderers
{
    [ForPropertyType((int)PropertyTypeEnum.Option)]
    public class OptionFilterRenderer : IFilterRenderer<OptionFilterViewModel>
    {
        public IViewComponentResult Render(OptionFilterViewModel model)
        {
            return new ViewViewComponentResult()
            {
                ViewName = "OptionFilterPartial"
            };        
        }

        public IViewComponentResult Render(FilterViewModel model)
        {
            return Render((OptionFilterViewModel) model);
        }
    }
}