﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebShop_NULL.Models.DomainModels;
using WebShop_NULL.Models.ViewModels;
using WebShop_NULL.Models.ViewModels.AdminPanelModels;

namespace WebShop_NULL.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly CommandService _commandService;
        private readonly ApplicationContext _dbContext;
        private readonly IWebHostEnvironment _appEnvironment;

        public AdminPanelController(CommandService commandService, ApplicationContext dbContext, IWebHostEnvironment appEnvironment)
        {
            _commandService = commandService;
            _dbContext = dbContext;
            _appEnvironment = appEnvironment;
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
        
        [HttpGet]
        public IActionResult CreateProduct()
        {
            var model = new AdminPanelCreateProductViewModel();
            var categories = _dbContext.Categories.Select(c => new CategoryDTO()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            model.Categories = categories;
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(AdminPanelCreateProductViewModel data)
        {
            var categories = _dbContext.Categories.Select(c => new CategoryDTO()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            data.Categories = categories;

            if (!ModelState.IsValid)
                return View(data);

            var product = new Product()
            {
                CategoryId = data.Category.Value,
                Name = data.ProductName,
                Description = data.ProductDescription,
                Price = (decimal) data.ProductPrice.Value,
                Rating = 5
            };

            var imageData = await CreateProductImageMetadata(data.Image);
            await _dbContext.ImageMetadata.AddAsync(imageData);

            await _dbContext.SaveChangesAsync();

            product.ImageId = imageData.Id;
            var attributeValues = new Dictionary<string, string>();

            foreach (var propertyInfo in data.PropertyInfos)
                attributeValues[propertyInfo.PropertyId.ToString()] = propertyInfo.Value;

            var jsonDoc = JsonDocument.Parse(JsonSerializer.SerializeToUtf8Bytes(attributeValues));
            product.AttributeValues = jsonDoc;

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            
            return RedirectToAction("Products");
        }

        public async Task<ImageMetadata> CreateProductImageMetadata(IFormFile imageFile)
        {
            var imageName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(imageFile.FileName);
            var virtualImagePath = Path.Combine("applicationData/productImages", imageName);
            var imagePath = Path.Combine(_appEnvironment.WebRootPath, virtualImagePath);

            await using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            var imageData = new ImageMetadata()
            {
                ImagePath = virtualImagePath,
                ContentType = imageFile.ContentType
            };

            return imageData;
        }

        [HttpGet("~/adminpanel/api/getproperties")]
        public IActionResult GetPropertyInfos(int categoryId)
        {
            if (!_dbContext.Categories.Any(c => c.Id == categoryId))
                return BadRequest();

            var properties = _dbContext.Categories
                .Where(c => c.Id == categoryId)
                .SelectMany(c => c.Properties)
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    PropertyType = p.Type.Name,
                    FilterInfo = p.FilterInfo.ToJsonString()
                }).ToList();

            return Json(properties);
        }

        public IActionResult Orders()
        {
            return BadRequest();
        }
    }
}