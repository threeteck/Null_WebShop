using Microsoft.AspNetCore.Mvc;
using WebShop_FSharp.ViewModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop_NULL.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult ChooseDeliveryMethod(OrderSummaryViewModel model)
        {
            return View(new OrderSummaryViewModel()
            {
                TotalPrice = 77350,
                TotalCount = 3,
            });
        }
    }
}
