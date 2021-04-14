using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModels;
using Microsoft.AspNetCore.Http;

namespace WebShop_NULL.Middleware
{
	public class CitiesMiddleware
	{
		private readonly RequestDelegate next;
		private List<string> cities;
		
		public CitiesMiddleware(RequestDelegate next)
		{
			this.next = next;
		}
		
		public async Task InvokeAsync(HttpContext context, ApplicationContext dbContext)
		{
			cities ??= dbContext.Cities.Select(x => x.Name).ToList();
			context.Items["cities"] = cities;
			await next.Invoke(context);
		}
	}
}