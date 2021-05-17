using Microsoft.AspNetCore.Mvc;
using WebShop_FSharp.ViewModels.CatalogModels;
using WebShop_NULL.Models.ViewModels;

namespace WebShop_NULL.Infrastructure.Filters
{
    public interface IFilterRenderer
    {
        IViewComponentResult Render(FilterViewModel model);
    }

    public interface IFilterRenderer<in T> : IFilterRenderer where T : FilterViewModel
    {
        IViewComponentResult Render(T model);
    }
}