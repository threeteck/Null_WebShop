using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DomainModels;
using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebShop_FSharp;
using WebShop_FSharp.ViewModels.AuthtorizationModels;

namespace WebShop_NULL.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailSender _sender;
        private readonly EmailConfirmationService _confirmationService;
        private readonly AuthenticationService _authenticationService;

        public AccountController(ILogger<AccountController> logger, ApplicationContext dbContext,
            IEmailSender sender, EmailConfirmationService confirmationService, 
            AuthenticationService authenticationService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _sender = sender;
            _confirmationService = confirmationService;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var modelStateBypass = false;
            if (model == null || !ModelState.IsValid)
            {
                var emailCookie = HttpContext.Request.Cookies["email"];
                var passwordCookie = HttpContext.Request.Cookies["password"];
                if (!string.IsNullOrEmpty(emailCookie) && !string.IsNullOrEmpty(passwordCookie))
                {
                    var user = _dbContext.Users.SingleOrDefault(x => x.Email == emailCookie);
                    if (user != null && !string.IsNullOrEmpty(passwordCookie) && HashPassword(passwordCookie) == user.HashedPassword)
                    {
                        model = new LoginModel {Email = emailCookie, Password = passwordCookie};
                        modelStateBypass = true;
                    }
                }
            }

            if (ModelState.IsValid || modelStateBypass)
            {
                User user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u =>
                    u.Email == model.Email && u.HashedPassword == HashPassword(model.Password));
                if (user == null)
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
                else if (!user.IsConfirmed)
                {
                    ModelState.AddModelError("", "Email не подтвержден");
                }
                else
                {
                    await _authenticationService.Authenticate(user,model.RememberMe != null);
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
                    UserRole userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(r => r.Name == "user");
                    user = new User
                    {
                        Email = model.Email,
                        Name = model.Name,
                        Surname = model.Surname,
                        HashedPassword = HashPassword(model.Password),
                        IsConfirmed = false,
                        Role = userRole,
                        Basket = new List<ShoppingCartEntry>(),
                    };
                    if (userRole != null)
                        user.Role = userRole;
                    var image = ImageMetadata.DefaultImage;
                    _dbContext.ImageMetadata.Add(image);
                    user.Image = image;
                    _dbContext.Users.Add(user);
                    await _dbContext.SaveChangesAsync();

                    return Redirect($"{Url.Action("ConfirmEmail")}?userId={user.Id}");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return RedirectToAction("Register");

            if (user.IsConfirmed)
                return RedirectToAction("Index", "Home"); //TODO: Redirect to Personal page
            
            var key = _confirmationService.GenerateEmailConfirmationToken(user.Id);
            var success = await _sender.SendEmailAsync(user.Email, "Подтверждение Email",
                $"Перейдите по ссылке для окончания регистрации: \n {Url.Action("EmailConfirmationEnd", "Account", null, Request.Scheme)}?key={key}&userId={userId}");
            if (!success)
                ModelState.AddModelError("", $"Письмо не может быть отправлено, т.к оно заблокированно по подозрению в спаме.\n {Url.Action("EmailConfirmationEnd", "Account", null, Request.Scheme)}?key={key}&userId={userId}");
            
            return View(model: user.Email);
        }

        public async Task<IActionResult> EmailConfirmationEnd(string key, int userId)
        {
            var actualUserId = _confirmationService.ConfirmEmail(key);

            var user = _dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == userId);
            if (user != null && actualUserId == userId)
            {
                user.IsConfirmed = true;
                await _dbContext.SaveChangesAsync();
                await _authenticationService.Authenticate(user,false);
            }
            else ModelState.AddModelError("","Your token is expired. Try again");

            return View(userId);
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();
            return RedirectToAction("Login");
        }
        

        public static string HashPassword(string password)
        {
            if (password.Length == 1)
            {
                return password;
            }
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = Encoding.UTF8.GetBytes("considerYourself-Salted");
            argon2.DegreeOfParallelism = 8;
            argon2.Iterations = 4;
            argon2.MemorySize = 1024 * 1024;

            return Encoding.UTF8.GetString(argon2.GetBytes(16));
        }
    }
}