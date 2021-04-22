using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop_NULL.Models.ViewModels.AdminPanelModels;

namespace WebShop_NULL.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly CommandService _commandService;

        public AdminPanelController(CommandService commandService)
        {
            _commandService = commandService;
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

        public IActionResult Products()
        {
            return View();
        }
        public IActionResult GetAdminMenu()
        {
            return PartialView("_GetAdminMenu");
        }
        public IActionResult CreateProduct()
        {
            return View();
        }
    }
}