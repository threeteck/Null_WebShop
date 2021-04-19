using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebShop_NULL.Controllers
{
    public class AdminPanel : Controller
    {
        private CommandService _commandService;

        public AdminPanel(CommandService commandService)
        {
            _commandService = commandService;
        }

        [HttpGet("/admin/commandLine")]
        public IActionResult CommandLine()
        {
            return View(false);
        }
        
        [HttpPost("/admin/commandLine")]
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
    }
}