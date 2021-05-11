using System.Diagnostics;
using DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sem2_FSharp.ViewModels;

//F# version exists, currently doesn't work. TODO fix F# version
namespace sem2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}