using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebShop_NULL.Models.ViewModels;

namespace WebShop_NULL.Infrastructure.Filters
{
    public class FilterRendererDispatcherViewComponent : ViewComponent
    {
        private FilterMapper<IFilterRenderer> _renderersMap;

        public FilterRendererDispatcherViewComponent(FilterMapper<IFilterRenderer> renderersMap)
        {
            _renderersMap = renderersMap;
        }

        public IViewComponentResult Invoke(FilterViewModel model)
        {
            Type rendererType = null;
            if (_renderersMap.ContainsId(model.PropertyId))
                rendererType = _renderersMap.GetFilterForProperty(model.PropertyId);
            else if (_renderersMap.ContainsType(model.PropertyType))
                rendererType = _renderersMap.GetFilterForType(model.PropertyType);
            else return new ContentViewComponentResult($"Cannot render filter for property {model.PropertyName}");

            var renderer = (IFilterRenderer)_renderersMap.GetFilterFactory(rendererType)();

            var expectedModelType = rendererType.GetInterfaces()
                .Single(i => i.IsGenericType &&
                             i.GetGenericTypeDefinition() == typeof(IFilterRenderer<>))
                .GetGenericArguments()[0];
            
            if(expectedModelType != model.GetType())
                throw new Exception($"Renderer '{rendererType.Name}' was expecting view model of type '{expectedModelType.Name}', but received '{model.GetType().Name}'");
            
            var result = renderer.Render(model);
            if (result is ViewViewComponentResult viewResult)
                result = View(viewResult.ViewName, model);
            return result;
        }
    }
}