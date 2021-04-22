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
        public IActionResult Index()
        {
            return View("Catalog");
        }
        [Route("~/category")]
        public IActionResult GetAllProductsFromCategory(string category)
        {
            var products = _dbContext.Products
                .Where(x => x.Category.Name.ToLower() == category.ToLower()).ToList();
            var model = new ProductsViewModel()
            {
                ProductList = products
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