using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DomainModels;
using WebShop_FSharp;
using WebShop_NULL.Models.ViewModels;
using WebShop_NULL.Models.ViewModels.СatalogModels;

namespace WebShop_NULL.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<CatalogController> _logger;
        private readonly int _productsPerPage = 6;
        
        public CatalogController(ILogger<CatalogController> logger, ApplicationContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        // GET
        public async Task<IActionResult> Index(int? categoryId = null, int page = 0, int sortingOption = 0)
        {
            var categories = _dbContext.Categories
                .Select(c => new CategoryDTO(c.Id, c.Name))
                .ToList();
            var query = _dbContext.Products.Select(p => p);
            if (categoryId != null)
                query = query.Where(p => p.Category.Id == categoryId.Value);

            if(sortingOption == 0)
                query = query.OrderByDescending(p => p.Rating).ThenBy(p => p.Name);
            else
                query = query.OrderBy(p => p.Price).ThenBy(p => p.Name);

            int count = query.Count();
            
            query = query.Skip(page * _productsPerPage).Take(_productsPerPage);

            var products = query.Select(p => new ProductCardDTO()
            {
                Id = p.Id,
                Price = p.Price,
                Name = p.Name,
                ImagePath = p.Image.ImagePath,
                Rating = p.Rating
            }).ToList();

            CategoryDTO category = null;
            if (categoryId != null)
                category = categories.FirstOrDefault(c => c.Id == categoryId);
            
            var model = new CatalogViewModel()
            {
                Categories = categories,
                Category = category,
                ProductList = products,
                Page = page,
                NumberOfPages = ((count - 1) / _productsPerPage) + 1
            };
            return View("Catalog", model);
        }

        [HttpGet("~/product/{productId}")]
        public async Task<IActionResult> ProductPage(int productId)
        {
            if (productId == -1)
            {
                RedirectToAction("Index", "Home");
            }

            var productModel = _dbContext.Products.ById(productId)
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Category = new CategoryDTO(p.Category.Id, p.Category.Name),
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                })
                .FirstOrDefault();
            
            if (productModel == null)
            {
                return StatusCode(404);
            }
            
            return View(productModel);
        }
    }
}