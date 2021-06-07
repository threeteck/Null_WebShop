using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModels;
using WebShop_FSharp;
using WebShop_FSharp.ViewModels.CatalogModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace WebShop_NULL.Views.Catalog.ViewComponents
{
    public class AddToBasketBtnViewComponent:ViewComponent
    {
        private readonly ApplicationContext _context;
        public AddToBasketBtnViewComponent(ApplicationContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(int userId,int productId)
        {
            var isInBasket = isProductInBasket(userId, productId);
            var viewModel = new AddToBasketBtnViewModel()
            {
                UserId = userId,
                ProductId = productId,
                IsInBasket = isInBasket,
            };
            var modelName = "_addToBasketBtn";
            return View(modelName, viewModel);
        }
        private bool isProductInBasket(int userId, int productId)
        {
            var basket = _context.Users
                .ById(userId)
                .Select(s => s.Basket)
                .FirstOrDefault();
            return basket.FirstOrDefault(x => x.ProductId == productId) != null;
                
        } 

    }
}
