using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebShop_NULL.Controllers
{
    public class AdminPanelController : Controller
    {
        private CommandService _commandService;

        public AdminPanelController(CommandService commandService)
        {
            _commandService = commandService;
        }

        [HttpGet]
        public IActionResult CommandLine()
        {
            return View(false);
        }
        
        [HttpPost]
        public IActionResult CommandLine(string command)
        {
            if(!_commandService.ExecuteCommand(command))
                ModelState.AddModelError("", "");
            return View(true);
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