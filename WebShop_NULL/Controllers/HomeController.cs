using System.Diagnostics;
using DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebShop_NULL.Models;
using WebShop_NULL.Models.ViewModels;
using WebShop_FSharp.ViewModels;

//F# version exists, currently doesn't work. TODO fix F# version
namespace WebShop_NULL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationContext dbContext)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}