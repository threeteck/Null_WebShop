﻿using DomainModels;
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
            var basketProducts = _dbContext.Users
                .Where(u => u.Id == User.GetId())
                .SelectMany(u => u.Basket).Select(p2 => new BasketProductViewModel()
                {
                    ProductId = p2.ProductId,
                    Name = p2.Product.Name,
                    Price = p2.Product.Price,
                    ImagePath = p2.User.Image.ImagePath,
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
            else return RedirectToAction("Index", "Catalog");
        }
        [Authorize]
        public async Task<IActionResult> SetQuantity(int userId, int productId, int quantity)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var basket = user.Basket;
            var basketProduct = basket.FirstOrDefault(b => b.ProductId == productId);
            basketProduct.Quantity = quantity;
            var basketProducts = basket.Select(p2 => new BasketProductViewModel()
            {
                ProductId = p2.ProductId,
                Name = p2.Product.Name,
                Price = p2.Product.Price,
                ImagePath = p2.User.Image.ImagePath,
                Quantity = p2.Quantity,
                Sum = p2.Product.Price * p2.Quantity
            }).ToList();
            return View("Index",new BasketViewModel() 
            { 
                Products = basketProducts,
                TotalSum = basketProducts.Sum(p=>p.Sum),
                TotalQuantity = basketProducts.Sum(p=>p.Quantity),
            });
        }
    }
}