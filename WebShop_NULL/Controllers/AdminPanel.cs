using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop_NULL.Models.ViewModels.AdminPanelModels;

namespace WebShop_NULL.Controllers
{
    public class AdminPanel : Controller
    {
        private readonly CommandService _commandService;

        public AdminPanel(CommandService commandService)
        {
            _commandService = commandService;
        }

        [HttpGet("/admin/commandLine")]
        public IActionResult CommandLine()
        {
            return View(CommandLineResponse.Empty());
        }
        
        [HttpPost("/admin/commandLine")]
        public IActionResult CommandLine(string command)
        {
            var response = CommandLineResponse.Empty();
            if (!_commandService.TryExecuteCommand(command, out var message))
                response = CommandLineResponse.Failure(message);
            else response = CommandLineResponse.Success(message);
            return View(response);
        }
    }
}