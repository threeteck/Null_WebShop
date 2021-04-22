using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop_NULL.Models.ViewModels;
using WebShop_NULL.Models.ViewModels.AdminPanelModels;

namespace WebShop_NULL.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly CommandService _commandService;
        private readonly ApplicationContext _dbContext;

        public AdminPanelController(CommandService commandService, ApplicationContext dbContext)
        {
            _commandService = commandService;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return RedirectToAction("CommandLine");
        }

        [HttpGet]
        public IActionResult CommandLine()
        {
            return View(CommandLineResponse.Empty());
        }
        
        [HttpPost]
        public IActionResult CommandLine(string command)
        {
            var response = CommandLineResponse.Empty();
            if (!_commandService.TryExecuteCommand(command, out var message))
                response = CommandLineResponse.Failure(message);
            else response = CommandLineResponse.Success(message);
            return View(response);
        }
        
        public IActionResult Products(string category, string query, int filterOption = 0)
        {
            var model = new AdminPanelProductsViewModel();
            model.Category = category;
            model.Query = query;
            model.FilterOption = filterOption;

            var categories = _dbContext.Categories.Select(c => new CategoryDTO()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            model.Categories = categories;

            var productsQuery = _dbContext.Products.Select(x => x);
            if (category != null && category != "all" && int.TryParse(category, out var categoryId))
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(query))
            {
                if (filterOption == 0)
                    productsQuery = productsQuery.Where(p => p.Name.ToLower().Contains(query.ToLower()));
                else if (filterOption == 1 && double.TryParse(query, out var ratingQuery))
                    productsQuery = productsQuery.Where(p => p.Rating == (decimal) ratingQuery);
                else if (filterOption == 2 && double.TryParse(query, out var priceQuery))
                    productsQuery = productsQuery.Where(p => p.Price == (decimal) priceQuery);
            }

            var products = productsQuery.Select(p => new AdminPanelProductDTO()
            {
                Id = p.Id,
                Name = p.Name,
                CategoryName = p.Category.Name,
                Price = (double)p.Price,
                Rating = (double)p.Rating
            }).ToList();

            model.Products = products;
            
            return View(model);
        }
        public IActionResult GetAdminMenu()
        {
            return PartialView("_GetAdminMenu");
        }
        public IActionResult CreateProduct()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return BadRequest();
        }
    }
}