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
        public async Task<IActionResult> Index(string category = null)
        {
            var categories = await _dbContext.Categories.Select(c => c.Name).ToListAsync();
            List<Product> products;
            if (category != null)
            {
                products = await _dbContext.Products.Where(p => p.Category.Name == category).Include(p => p.Image).ToListAsync();
            }
            else
            {
                products = await _dbContext.Products.OrderBy(p => p.Rating).Take(6).Include(p => p.Image).ToListAsync();
            }
            var model = new CatalogViewModel()
            {
                Categories = categories,
                Category = category,
                ProductList = products,
            };
            return View("Catalog",model);
        }
       /* public async Task<IActionResult> Catalog(string category)
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
        }*/
        public async Task<IActionResult> ProductPage(string category, int ProductId = -1)
        {
            if (ProductId == -1)
            {
                RedirectToAction("Index", "Home");
            }
            var product = await _dbContext.Products.Include(p=>p.Image).FirstOrDefaultAsync(x => x.Id == ProductId);
            if (product == null)
            {
                return StatusCode(404);
            }

            var productModel = new ProductViewModel()
            {
                Category = category,
                Name =  product.Name,
                Description = product.Description,
                Image = product.Image.ImagePath,
                Price = product.Price,
            };
            return View(productModel);
        }
    }
}