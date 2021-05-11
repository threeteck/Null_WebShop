using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DomainModels;
using Microsoft.AspNetCore.Authorization;
using sem2_FSharp;
using sem2_FSharp.ViewModels;
using sem2_FSharp.ViewModels.CatalogModels;

namespace sem2.Controllers
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
            var query = _dbContext.Products.Select(p => p);

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
            
            var model = new CatalogViewModel()
            {
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

            var result = _dbContext.Products.ById(productId)
                .Select(p => new
                {
                    Product = p,
                    Genres = p.Genres,
                    ImagePath = p.Image.ImagePath,
                    Reviews = p.Reviews.OrderByDescending(review => review.Date)
                        .Select(review => new ReviewDTO()
                    {
                        Content = review.Content,
                        Rating = review.Rating,
                        UserId = review.UserId,
                        UserImagePath = review.User.Image.ImagePath,
                        UserName = $"{review.User.Name} {review.User.Surname}"
                    })
                }).FirstOrDefault();
            
            if (result == null)
            {
                return StatusCode(404);
            }
            
            var productModel = new ProductViewModel()
            {
                Id = result.Product.Id,
                Name = result.Product.Name,
                Description = result.Product.Description,
                Price = result.Product.Price,
                ImagePath = result.ImagePath,
                Rating = result.Product.Rating,
                Genres = result.Product.Genres,
                Reviews = result.Reviews
            };

            return View(productModel);
        }

        [Authorize]
        [HttpPost("~/product/{productId}/sendReview")]
        public async Task<IActionResult> SendReview(int productId, string content, int rating)
        {
            int userId = User.GetId();
            var review = new Review()
            {
                Content = content,
                Date = DateTime.Now,
                FilmId = productId,
                UserId = userId,
                Rating = rating
            };

            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();

            return Redirect(Url.Content($"~/product/{productId}"));
        }
    }
}