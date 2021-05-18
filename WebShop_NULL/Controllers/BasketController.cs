using DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop_FSharp;
using WebShop_FSharp.ViewModels.BasketModels;

namespace WebShop_NULL.Controllers
{
    public class BasketController : Controller
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<ProfileController> _logger;

        public BasketController(ILogger<ProfileController> logger, ApplicationContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        [Authorize]
        public IActionResult Index()
        {
            var products = _dbContext.Users
                .Where(u => u.Id == User.GetId())
                .Select(p => p.Basket)
                .Select(p => p
                    .Select(p2 => new BasketProductViewModel()
                    {
                        ProductId = p2.Id,
                        Name = p2.Name,
                        Price = p2.Price,
                        ImagePath = p2.Image.ImagePath,
                        Quantity = p.Where(x => x.Id == p2.Id).Count(),
                        Sum = p.Where(x=>x.Id==p2.Id).Sum(s=>s.Price),
                    }))
                .FirstOrDefault();

            if (products.Count()==0)
            {
                return View(null);
            }
            return View(new BasketViewModel() 
            { 
                Products = products,
                TotalSum = products.Sum(p=>p.Sum),
                TotalQuantity = products.Sum(p=>p.Quantity),
            });
        }
        [Authorize]
        public async Task<IActionResult> RemoveProducts(int userId,int productId)
        {
            var user = _dbContext.Users.Include(u=>u.Basket).FirstOrDefault(u=>u.Id==userId);
            var products = _dbContext.Products.Where(p => p.Id == productId);
            foreach(var p in products)
            {
                user.Basket.Remove(p);
            }
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
           
        }
        [Authorize]
        public async Task<IActionResult> SetQuantity(int userId, int productId, int quantity)
        {
            var user = _dbContext.Users.Include(u => u.Basket).FirstOrDefault(u => u.Id == userId);
            var products = _dbContext.Products.Where(p => p.Id == productId);
            var product = products.FirstOrDefault();
            var count = quantity-products.Count();
            if (quantity < 1)
            {
                return StatusCode(404);
            }
            else if (quantity > products.Count())
            {
                
                for(int i=0; i < count; i++)
                {
                    user.Basket.Add(new Product(
                        product.Id,
                        product.Name,
                        product.Description,
                        product.Price,
                        product.Rating,
                        product.CategoryId,
                        product.Category,
                        product.ImageId,
                        product.Image,
                        product.AttributeValues,
                        product.InBasketOf,
                        product.Reviews
                        ));
                }
            }
            else
            {
                for(int i = 0; i < count; i++)
                {
                    user.Basket.Remove(product);
                }
            }
            await _dbContext.SaveChangesAsync();
            return StatusCode(200);

        }
    }
}
