using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DomainModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebShop_FSharp;
using WebShop_NULL.Models.AuthtorizationModels;

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
                    await Authenticate(user,model.RememberMe);
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
                    var image = ImageMetadata.DefaultImage;
                    _dbContext.ImageMetadata.Add(image);
                    await _dbContext.SaveChangesAsync();

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

        public IActionResult EmailConfirmationEnd(string key, int userId)
        {
            var actualUserId = _confirmationService.ConfirmEmail(key);
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null && actualUserId == userId)
            {
                user.IsConfirmed = true;
                _dbContext.SaveChanges();
            }
            else ModelState.AddModelError("","Your token is expired. Try again");

            return View(userId);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        private async Task Authenticate(User user, bool rememberMe)
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
            if(!rememberMe)
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            else
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id)
                    ,new AuthenticationProperties
                    {
                        IsPersistent = true
                    });
            }
        }

        public static string HashPassword(string password) 
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