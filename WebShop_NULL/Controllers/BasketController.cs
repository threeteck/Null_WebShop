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
using WebShop_FSharp.ViewModels.OrderModels;

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
            var basketProducts = _dbContext.Users
                .Where(u => u.Id == User.GetId())
                .SelectMany(u => u.Basket).Select(p2 => new BasketProductViewModel()
                {
                    ProductId = p2.ProductId,
                    Name = p2.Product.Name,
                    Price = p2.Product.Price,
                    ImagePath = p2.Product.Image.ImagePath,
                    Quantity = p2.Quantity,
                    Sum = p2.Product.Price * p2.Quantity
                }).ToList();

            if (!basketProducts.Any())
            {
                return View(null);
            }
            return View(new BasketViewModel() 
            { 
                Products = basketProducts,
                TotalSum = basketProducts.Sum(p=>p.Sum),
                TotalQuantity = basketProducts.Sum(p=>p.Quantity),
            });
        }
        [Authorize]
        public async Task<IActionResult> RemoveProducts(int userId, int productId)
        {
            var user = _dbContext.Users.Include(u => u.Basket).FirstOrDefault(u => u.Id == userId);
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == productId);
            var entry = _dbContext.ShoppingCartEntries.FirstOrDefault(e => e.UserId == userId && e.ProductId == productId);
            if (user != null && product != null && entry != null)
            {
                _dbContext.ShoppingCartEntries.Remove(entry);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("ProductPage", "Catalog", new { productId = productId });
            }
            else return RedirectToAction("Index", "Basket");
        }
        [Authorize]
        
        [HttpGet]
        [Route("~/Basket/SetQuantity")]
        public async Task<IActionResult> SetQuantity([FromQuery] int userId, [FromQuery] int productId, [FromQuery] int quantity)
        {
            var cartEntry = _dbContext.ShoppingCartEntries.
                FirstOrDefault(entry => entry.UserId == userId && entry.ProductId == productId);
            if (cartEntry == null)
            {
                return View("Index");
            }
            cartEntry.Quantity = quantity;
            await _dbContext.SaveChangesAsync();
            return StatusCode(200);
        }
        public async Task<IActionResult> GetBasketMenuPartial(int userId)
        {
            var model = await _dbContext
                .Users
                .Where(s => s.Id == userId)
                .Select(s => new OrderSummaryViewModel()
                {
                    TotalCount = s.Basket.Sum(x => x.Quantity),
                    TotalPrice = s.Basket.Sum(x => x.Product.Price * x.Quantity)
                })
                .FirstOrDefaultAsync();
            return View("OrderSummary", model);
        }
    }
}
