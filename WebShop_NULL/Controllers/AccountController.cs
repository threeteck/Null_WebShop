using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebShop_NULL.Models.AuthtorizationModels;
using WebShop_NULL.Models.DomainModels;

namespace WebShop_NULL.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailSender _sender;
        private readonly EmailConfirmationService _confirmationService;

        public AccountController(ILogger<AccountController> logger, ApplicationContext dbContext, IEmailSender sender, EmailConfirmationService confirmationService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _sender = sender;
            _confirmationService = confirmationService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u =>
                    u.Email == model.Email && u.HashedPassword == HashPassword(model.Password));
                if (user == null || !user.IsConfirmed)
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
                else
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    user = new User
                    {
                        Email = model.Email,
                        Name = model.Name,
                        Surname = model.Surname,
                        HashedPassword = HashPassword(model.Password),
                        IsConfirmed = false
                    };
                    UserRole userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(r => r.Name == "user");
                    if (userRole != null)
                        user.Role = userRole;
                    _dbContext.Users.Add(user);
                    await _dbContext.SaveChangesAsync();

                    return Redirect($"{Url.Action("ConfirmEmail")}?userId={user.Id}");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return RedirectToAction("Register");

            if (user.IsConfirmed)
                return RedirectToAction("Index", "Home"); //TODO: Redirect to Personal page
            
            var key = _confirmationService.GenerateEmailConfirmationToken(user.Id);
            await _sender.SendEmailAsync(user.Email, "Подтверждение Email",
                $"Перейдите по ссылке для окончания регистрации: \n {Url.Action("EmailConfirmationEnd")}?key={key}");
            return View(user.Email);
        }

        public IActionResult EmailConfirmationEnd(string key)
        {
            var userId = _confirmationService.ConfirmEmail(key);
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.IsConfirmed = true;
                _dbContext.SaveChanges();
            }
            else ModelState.AddModelError("","Your token is expired. Try again");

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
                new Claim("username", user.Name),
                new Claim("id",user.Id.ToString())
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        private string HashPassword(string password) 
        {
            var hashBuilder = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                var result = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                foreach (var b in result)
                    hashBuilder.Append(b.ToString("x2"));
            }

            return hashBuilder.ToString();
        }
    }
}