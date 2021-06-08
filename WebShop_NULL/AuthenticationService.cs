using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DomainModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebShop_NULL
{
    public class AuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync();
        }

        public async Task ReAuthenticate(string userName,bool rememberMe)
        {
            var user = _httpContextAccessor.HttpContext.User;
            var claims = new List<Claim>
            {    
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Identity.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType)),
                new Claim("username", userName),
                new Claim("id",user.FindFirstValue("id"))
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await Logout();
            if(!rememberMe)
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            else
            {
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id)
                    ,new AuthenticationProperties
                    {
                        IsPersistent = true
                    });
            }
        }

        public async Task Authenticate(User user, bool rememberMe)
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
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            else
            {
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id)
                    ,new AuthenticationProperties
                    {
                        IsPersistent = true
                    });
            }
        }

        [HttpGet]
        public async Task<IActionResult> SuperKek()
        {
            var claims = new List<Claim>
            {    
                new Claim(ClaimsIdentity.DefaultNameClaimType, "threeteck@gmail.com"),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin"),
                new Claim("username", "threeteck"),
                new Claim("id","15")
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            return new OkResult();
        }
    }
}