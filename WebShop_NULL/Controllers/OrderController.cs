using Microsoft.AspNetCore.Mvc;
using WebShop_FSharp.ViewModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop_FSharp;
using DomainModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace WebShop_NULL.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationContext _dbContext;
        private readonly IAddressValidator _addressValidator;

        public OrderController(ApplicationContext context, IAddressValidator addressValidator)
        {
            _dbContext = context;
            _addressValidator = addressValidator;
        }
        public IActionResult ChooseDeliveryMethod(OrderSummaryViewModel model)
        {

            return View(model);

        }
        [HttpGet]
        public IActionResult DeliveryToShop()
        {
            var userId = User.GetId();
            var user = _dbContext.Users.ById(userId).FirstOrDefault();
            var cities = _dbContext.Cities.OrderBy(c => c.Name).Select(c => c.Name).ToList();
            var entries = _dbContext.ShoppingCartEntries.Where(s => s.UserId == userId).Select(s => new { count = s.Quantity, price = s.Product.Price }).ToList();
            var shops = _dbContext.Shops.Where(s => s.CityName == cities[0]).Select(u => u.Address).OrderBy(s => s).ToList();
            var model = new DeliveryToShopViewModel()
            {
                FirstName = user?.Name,
                LastName = user?.Surname,
                Email = user?.Email,
                City = "Город",
                Cities = (cities.Count > 0 ? new SelectList(cities, cities[0]) : null),
                ShopAddress = "Адрес",
                ShopAddresses = shops.Count > 0 ? new SelectList(shops, shops[0]) : null,
                TotalCount = entries.Sum(e => e.count),
                TotalPrice = entries.Sum(e => e.count * e.price),
            };
            return View("CreateToShopOrder", model);
        }
        [HttpPost]
        public async Task<IActionResult> DeliveryToShop(DeliveryToShopViewModel model)
        {
            var userId = User.GetId();
            IOrderStates orderStates = new ToShopDeliveryOrder();
            var entry = _dbContext.ShoppingCartEntries.Where(u => u.UserId == userId).Include(e => e.Product).ToList();
            var order = new Order()
            {
                UserId = userId,
                DeliveryMethod = DeliveryMethods.DeliveryToShop.GetString,
                CreateDate = DateTime.Now,
                State = orderStates.GetDefaultState(),
                OrderItems = new Collection<OrderItems>(entry.Select(s => new OrderItems()
                {
                    ProductName = s.Product.Name,
                    ProductPrice = s.Product.Price,
                    ProductQuantity = s.Quantity,
                    ProductId = s.ProductId,

                }).ToList()),
                TotalCount = entry.Sum(s => s.Quantity),
                TotalPrice = entry.Sum(s => s.Quantity * s.Product.Price),
                Address = model.ShopAddress,

            };
            _dbContext.Orders.Add(order);
            _dbContext.ShoppingCartEntries.RemoveRange(entry);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Orders", "Profile");
        }

        public IActionResult GetShops(string cityName)
        {
            var model = _dbContext.Shops.Where(s => s.CityName == cityName).OrderBy(o => o.Address).Select(s => s.Address).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult DeliveryToHome()
        {
            var userId = User.GetId();
            var user = _dbContext.Users.ById(userId).FirstOrDefault();
            var entries = _dbContext.ShoppingCartEntries.Where(s => s.UserId == userId).Select(s => new { count = s.Quantity, price = s.Product.Price }).ToList();
            var model = new DeliveryToHomeViewModel()
            {
                FirstName = user?.Name,
                LastName = user?.Surname,
                Email = user?.Email,
                TotalCount = entries.Sum(e => e.count),
                TotalPrice = entries.Sum(e => e.count * e.price),
            };
            return View("CreateToHomeOrder", model);
        }
        [HttpPost]
        public async Task<IActionResult> DeliveryToHome(DeliveryToHomeViewModel model)
        {
            var userId = User.GetId();
            IOrderStates orderStates = new ToHomeDeliveryOrder();
            var entry = _dbContext.ShoppingCartEntries.Where(u => u.UserId == userId).Include(e => e.Product).ToList();
            var order = new Order()
            {
                UserId = userId,
                DeliveryMethod = DeliveryMethods.DeliveryToHome.GetString,
                CreateDate = DateTime.Now,
                State = orderStates.GetDefaultState(),
                OrderItems = new Collection<OrderItems>(entry.Select(s => new OrderItems()
                {
                    ProductName = s.Product.Name,
                    ProductPrice = s.Product.Price,
                    ProductQuantity = s.Quantity,
                    ProductId = s.ProductId,

                }).ToList()),
                TotalCount = entry.Sum(s => s.Quantity),
                TotalPrice = entry.Sum(s => s.Quantity * s.Product.Price),
                Address = model.Address,

            };
            _dbContext.Orders.Add(order);
            _dbContext.ShoppingCartEntries.RemoveRange(entry);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Orders", "Profile");
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyAddress(string address)
        {

            var result = _addressValidator.IsAddressValid(address);
            return Json(result);
        }
    }
}
