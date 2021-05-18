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
    public class AddToBasketBtn:ViewComponent
    {
        private readonly ApplicationContext _context;
        public AddToBasketBtn(ApplicationContext context)
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
            return View("_addToBasketBtn", viewModel);
        }
        private bool isProductInBasket(int userId, int productId)
        {
            var user = _context.Users.Include(u => u.Basket).FirstOrDefault(u=>u.Id == userId);
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if(user!=null && product != null)
            {
                return user.Basket.Contains(product);
            }
            return false;
        } 

    }
}
