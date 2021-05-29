using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebShop_FSharp;
using WebShop_FSharp.ViewModels.AuthtorizationModels;
using WebShop_FSharp.ViewModels.ProfileModels;
using WebShop_FSharp.ViewModels.OrderModels;
using Microsoft.EntityFrameworkCore;

namespace WebShop_NULL.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<ProfileController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly AuthenticationService _authenticationService;

        public ProfileController(ILogger<ProfileController> logger, ApplicationContext dbContext, IWebHostEnvironment appEnvironment, AuthenticationService authenticationService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _appEnvironment = appEnvironment;
            _authenticationService = authenticationService;
        }

        [Route("~/profile/{userId:int?}")]
        public IActionResult Profile(int userId = -1)
        {
            if(userId == -1)
                if (User.Identity.IsAuthenticated)
                    userId = User.GetId();
                else return RedirectToAction("Index", "Home");
            
            var user = _dbContext.Users.ById(userId).FirstOrDefault();
            if (user == null)
                return RedirectToAction("Index", "Home");
            
            var model = new UserViewModel()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname
            };

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ProfileEdit()
        {
            var user = _dbContext.Users.ById(User.GetId()).FirstOrDefault();
            if (user == null)
                return RedirectToAction("Index", "Home");
            
            var model = new UserViewModel()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname
            };

            return View(model);
        }
        
        [HttpPost("~/profile/uploadImage")]
        [Authorize]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            var imageData = _dbContext.Users.ImageById(User.GetId()).FirstOrDefault();
            if (imageData == null)
                return BadRequest();
            
            var oldImagePath = Path.Combine(_appEnvironment.WebRootPath, imageData.ImagePath);

            var imageName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(image.FileName);
            var virtualImagePath = Path.Combine("applicationData/profileImages", imageName);
            var imagePath = Path.Combine(_appEnvironment.WebRootPath, virtualImagePath);
            
            if (Path.GetFileNameWithoutExtension(oldImagePath) != "default" && System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);

            await using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            imageData.ContentType = image.ContentType;
            imageData.ImagePath = virtualImagePath;

            await _dbContext.SaveChangesAsync();

            return Redirect(Url.Content("~/profile"));
        }

        [HttpGet("~/profile/{userId}/image")]
        public IActionResult GetImage(int userId)
        {
            var data = _dbContext.Users.ImageById(userId).FirstOrDefault();
            if (data == null)
                return BadRequest();

            return File(data.ImagePath, data.ContentType);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileEdit(UserViewModel data)
        {
            if (ModelState.IsValid)
            {
                var user = _dbContext.Users.ById(User.GetId()).FirstOrDefault();
                if (user == null)
                    return RedirectToAction("Index", "Home");

                user.Name = data.Name;
                user.Surname = data.Surname;

                _dbContext.SaveChanges();

                await _authenticationService.ReAuthenticate(user.Name,false);

                return Redirect("~/profile");
            }

            return RedirectToAction("ProfileEdit");
        }

        [Authorize]
        [HttpPost]
        public IActionResult ProfileEditPassword(PasswordChangeModel data)
        {
            var user = _dbContext.Users.ById(User.GetId()).FirstOrDefault();
            if (user == null)
                return RedirectToAction("Login", "Account");

            if (user.HashedPassword != AccountController.HashPassword(data.OldPassword))
            {
                TempData["PasswordNotMatch"] = "Старый пароль не совпадает.";
                ModelState.AddModelError("", "Старый пароль не совпадает.");
            }

            if (!ModelState.IsValid)
                return RedirectToAction("ProfileEdit");
            
            user.HashedPassword = AccountController.HashPassword(data.NewPassword);
            _dbContext.SaveChanges();
            TempData["PasswordChangeSuccess"] = true;

            return RedirectToAction("ProfileEdit");
        }

        [Authorize]
        public IActionResult Orders()
        {
            var userId = User.GetId();
            var orders = _dbContext.Orders.Where(o => o.UserId == userId);
            var model = orders.Select(o => new OrderInfoViewModel()
            {
                OrderId = o.Id,
                CreateDate = o.CreateDate,
                TotalPrice = o.TotalPrice,
                State = o.State,
            }).AsEnumerable();
            return View(model);
        }
        [Authorize]
        public IActionResult OrderPage(int orderId)
        {
            var order = _dbContext.Orders.Where(o => o.Id == orderId).Include(o => o.OrderItems).FirstOrDefault();
            var model = new OrderPageViewModel()
            {
                OrderId = orderId,
                OrderState = order.State,
                OrderItems = order.OrderItems,
                DeliveryMethod = order.DeliveryMethod,
                CreateDate = order.CreateDate,
                Address = order.Address,
                TotalCount = order.TotalCount,
                TotalPrice = order.TotalPrice,
            };
            return View(model);
        }
        
    }
}