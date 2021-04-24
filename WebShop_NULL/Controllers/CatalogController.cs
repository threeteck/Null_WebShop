using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DomainModels;
using WebShop_NULL.Models.ViewModels.СatalogModels;

namespace WebShop_NULL.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<AccountController> _logger;
        public CatalogController(ILogger<AccountController> logger, ApplicationContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        // GET
        public async Task<IActionResult> Index()
        {
            var products = await _dbContext.Products
                .OrderBy(x => x.Rating)
                .Take(6)
                .Include(p => p.Image)
                .ToListAsync();
            var model = new ProductsViewModel()
            {
                ProductList = products,
            };
            return View("Catalog",model);
        }
        public async Task<IActionResult> Catalog(string category)
        {
            var products = await _dbContext.Products
                .Where(x => x.Category.Name.ToLower() == category.ToLower())
                .Include(p=>p.Image).ToListAsync();
            var categories = await _dbContext.Categories
                .Include(c => c.Properties)
                .FirstOrDefaultAsync(c => c.Name.ToLower() == category.ToLower());
            var model = new ProductsViewModel()
            {
                ProductList = products,
                Category = category,
                Properties = categories.Properties,
            };
            return View(model);
        }
        [Route("~/category/{ProductId?}")]
        public async Task<IActionResult> ViewProductPage(int ProductId = -1)
        {
            if (ProductId == -1)
            {
                RedirectToAction("Index", "Home");
            }
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == ProductId);
            if (product == null)
            {
                return StatusCode(404);
            }

            var productModel = new ProductViewModel()
            {
                Name =  product.Name,
                Description = product.Description,
                ImageId = product.ImageId
            };
            return View(productModel);
        }
    }
}