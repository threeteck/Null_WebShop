using Microsoft.Extensions.DependencyInjection;
using WebShop_NULL.Models.ViewModels;

namespace WebShop_NULL.Infrastructure.Filters
{
    public static class ServiceCollectionFilterExtensions
    {
        public static IServiceCollection AddFilters(this IServiceCollection services)
        {
            services.AddSingleton<FilterMapper<FilterDTO>>();
            services.AddSingleton<FilterMapper<FilterViewModel>>();
            services.AddSingleton<FilterMapper<IFilterRenderer>>();
            services.AddSingleton<FilterViewModelProvider>();
            return services;
        }
    }
}