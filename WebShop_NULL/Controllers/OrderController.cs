using Microsoft.AspNetCore.Mvc;
using WebShop_FSharp.ViewModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop_FSharp;
using DomainModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebShop_NULL.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationContext _dbContext;

        public OrderController(ApplicationContext context)
        {
            _dbContext = context;
        }
        public IActionResult ChooseDeliveryMethod(OrderSummaryViewModel model)
        {
            return View(new OrderSummaryViewModel()
            {
                TotalPrice = 77350,
                TotalCount = 3,
            });
        }
        [HttpGet]
        public IActionResult DeliveryToShop()
        {
            var userId = User.GetId();
            var user = _dbContext.Users.ById(userId).FirstOrDefault();
            var cities = _dbContext.Cities.OrderBy(c=>c.Name).Select(c=>c.Name).ToList();
            var entries = _dbContext.ShoppingCartEntries.Where(s => s.UserId == userId).Select(s => new { count = s.Quantity, price=s.Product.Price}).ToList();
            var shops = _dbContext.Shops.Where(s => s.CityName == cities[0]).Select(u=>u.Address).OrderBy(s=>s).ToList();
            var model = new DeliveryToShopViewModel()
            {
                FirstName = user?.Name,
                LastName = user?.Surname,
                City = "Город",
                Cities = (cities.Count>0 ? new SelectList(cities, cities[0]):null),
                ShopAddress = "Адрес",
                ShopAddresses = shops.Count>0 ? new SelectList(shops, shops[0]):null,
                TotalCount = entries.Sum(e => e.count),
                TotalPrice = entries.Sum(e=>e.count*e.price),
            };
            return View("CreateToShopOrder",model);
        }
        public IActionResult StartCreateToHomeDeliveryOrder()
        {
            return null;
        }

        public IActionResult GetShops(string cityName)
        {
            var model = _dbContext.Shops.Where(s => s.CityName == cityName).OrderBy(o => o.Address).Select(s => s.Address).ToList();
            return View(model);
        }
    }
}
