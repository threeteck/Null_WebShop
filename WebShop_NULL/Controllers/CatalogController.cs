using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using WebShop_FSharp;
using WebShop_FSharp.ViewModels;
using WebShop_FSharp.ViewModels.CatalogModels;
using WebShop_NULL.Infrastructure.Filters;
using WebShop_NULL.Models;
using WebShop_NULL.Models.ViewModels;

namespace WebShop_NULL.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<CatalogController> _logger;
        private readonly FilterViewModelProvider _filterViewModelProvider;
        private readonly int _productsPerPage = 9;
        private readonly int _reviewsPerPage = 6;
        
        public CatalogController(ILogger<CatalogController> logger, ApplicationContext dbContext, FilterViewModelProvider filterViewModelProvider)
        {
            _logger = logger;
            _dbContext = dbContext;
            _filterViewModelProvider = filterViewModelProvider;
        }
        // GET
        public async Task<IActionResult> Index(CatalogDTO catalogDto)
        {
            var categories = _dbContext.Categories
                .Select(c => new CategoryDTO(c.Id, c.Name))
                .ToList();
            var query = _dbContext.Products.Select(p => p);
            if (catalogDto.CategoryId != null)
                query = query.Where(p => p.Category.Id == catalogDto.CategoryId.Value);
            if (!string.IsNullOrWhiteSpace(catalogDto.Query))
                query = query.Where(p => p.Name.ToLower().Contains(catalogDto.Query.ToLower()));
            
            query = query.Where(p => p.Price >= catalogDto.PriceMin && p.Price <= catalogDto.PriceMax);
            query = query.Where(p => p.Rating >= catalogDto.RatingMin && p.Rating <= catalogDto.RatingMax);

            if(catalogDto.Filters != null)
                foreach (var filter in catalogDto.Filters)
                    query = query.Where(filter.Expression);
            
            if(catalogDto.SortingOption == 0)
                query = query.OrderByDescending(p => p.Rating).ThenBy(p => p.Name);
            else
                query = query.OrderBy(p => p.Price).ThenBy(p => p.Name);

            int count = query.Count();
            
            query = query.Skip(catalogDto.Page * _productsPerPage).Take(_productsPerPage);

            var products = query.Select(p => new ProductCardDTO()
            {
                Id = p.Id,
                Price = p.Price,
                Name = p.Name,
                ImagePath = p.Image.ImagePath,
                Rating = p.Rating
            }).ToList();

            CategoryDTO category = null;
            if (catalogDto.CategoryId != null)
                category = categories.FirstOrDefault(c => c.Id == catalogDto.CategoryId);

            var model = new CatalogViewModel()
            {
                Categories = categories,
                Category = category,
                ProductList = products,
                Page = catalogDto.Page,
                NumberOfPages = ((count - 1) / _productsPerPage) + 1,
                Query = catalogDto.Query,
                PriceMin = catalogDto.PriceMin,
                PriceMax = catalogDto.PriceMax,
                RatingMin = catalogDto.RatingMin,
                RatingMax = catalogDto.RatingMax,
                ProductsCount = count
            };
            
            if (catalogDto.CategoryId != null)
            {
                var properties = _dbContext.Categories.ById(catalogDto.CategoryId.Value)
                    .SelectMany(cat => cat.Properties).Include(p => p.Type)
                    .ToList();
                if (catalogDto.Filters != null && catalogDto.Filters.Count > 0)
                {
                    var filterDtoMap = catalogDto.Filters
                        .ToDictionary(dto => dto.PropertyId, dto => dto);
                    model.Filters = properties
                        .Select(p => _filterViewModelProvider.GetFilterViewModel(p, filterDtoMap[p.Id]))
                        .ToList();
                }
                else
                {
                    model.Filters = properties
                        .Select(p => _filterViewModelProvider.GetFilterViewModel(p))
                        .ToList();
                }
            }
            
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
                    Category = p.Category,
                    Properties = p.Category.Properties,
                    ImagePath = p.Image.ImagePath,
                    ReviewsCount = p.Reviews.Count,
                }).FirstOrDefault();
            
            if (result == null)
            {
                return StatusCode(404);
            }
            
            var productModel = new ProductViewModel()
            {
                Id = result.Product.Id,
                Category = new CategoryDTO(result.Category.Id, result.Category.Name),
                Name = result.Product.Name,
                Description = result.Product.Description,
                Price = result.Product.Price,
                ImagePath = result.ImagePath,
                Rating = result.Product.Rating,
                Properties = GetPropertyValues(result.Product.AttributeValues)
                    .Join(result.Properties, dict => int.Parse(dict.Key), 
                        prop => prop.Id,
                        (dict, prop) => new PropertyDTO()
                        {
                            Name = prop.Name,
                            Value = dict.Value
                        }),
                ReviewsTotalPages = ((result.ReviewsCount - 1) / _reviewsPerPage) + 1
            };

            return View(productModel);
        }
        
        [HttpGet("~/product/{productId}/reviews")]
        public IActionResult GetReviews(int productId, int page = 0)
        {
            var reviews = _dbContext.Reviews
                .Where(review => review.ProductId == productId)
                .OrderByDescending(review => review.Date)
                .Skip(_reviewsPerPage * page)
                .Take(6)
                .Select(review => new ReviewDTO()
                {
                    Content = review.Content,
                    Rating = review.Rating,
                    UserId = review.UserId,
                    UserImagePath = review.User.Image.ImagePath,
                    UserName = $"{review.User.Name} {review.User.Surname}"
                })
                .ToList();

            return Json(reviews);
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
                ProductId = productId,
                UserId = userId,
                Rating = rating
            };

            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();

            var reviewCount = _dbContext.Reviews
                .Count(r => r.ProductId == productId);
            
            var product = _dbContext.Products.ById(productId).FirstOrDefault();
            if (product == null)
                return BadRequest();
            
            var oldRating = product.Rating;
            product.Rating = (oldRating * (reviewCount - 1) + rating) / reviewCount;

            await _dbContext.SaveChangesAsync();
            return Redirect(Url.Content($"~/product/{productId}"));
        }
        
        public Dictionary<string, string> GetPropertyValues(JsonDocument jDoc)
        {
            return JsonSerializer.Deserialize<Dictionary<string, object>>(jDoc.ToJsonString())
                .ToDictionary(pair => pair.Key, pair => pair.Value.ToString());
        }
        
        [HttpGet("~/{categoryId:int}/search")]
        public IActionResult Search(int categoryId)
        {
            var model = new SearchViewModel();
            var properties = _dbContext.Categories.ById(categoryId)
                .SelectMany(cat => cat.Properties).Include(p => p.Type)
                .ToList();
            model.Filters = properties
                .Select(p => _filterViewModelProvider.GetFilterViewModel(p))
                .ToList();
            
            return View(model);
        }

        [HttpPost("~/{categoryId:int}/search")]
        public IActionResult Search(int categoryId, List<FilterDTO> filters)
        {
            var model = new SearchViewModel();
            var properties = _dbContext.Categories.ById(categoryId)
                .SelectMany(cat => cat.Properties).Include(p => p.Type)
                .ToList();
            var filterDtoMap = filters
                .ToDictionary(dto => dto.PropertyId, dto => dto);
            model.Filters = properties
                .Select(p => _filterViewModelProvider.GetFilterViewModel(p, filterDtoMap[p.Id]))
                .ToList();
            return View(model);
        }
    }
}