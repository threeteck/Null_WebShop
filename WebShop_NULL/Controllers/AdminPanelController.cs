using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebShop_FSharp;
using WebShop_FSharp.ViewModels;
using WebShop_FSharp.ViewModels.AdminPanelModels;
using WebShop_NULL.Models.ViewModels.AdminPanelModels;
using Property = DomainModels.Property;

namespace WebShop_NULL.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminPanelController : Controller
    {
        private readonly CommandService _commandService;
        private readonly ApplicationContext _dbContext;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IEmailSender _emailSender;

        public AdminPanelController(CommandService commandService, ApplicationContext dbContext, IWebHostEnvironment appEnvironment, IEmailSender emailSender)
        {
            _commandService = commandService;
            _dbContext = dbContext;
            _appEnvironment = appEnvironment;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return RedirectToAction("CommandLine");
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

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(AdminPanelCreateCategoryViewModel data)
        {
            if (!ModelState.IsValid)
                return View();

            var propertyTypes = _dbContext.PropertyTypes.ToList()
                .ToDictionary(type => type.Name, type => type);

            var properties = new List<Property>();
            foreach (var propertyInfo in data.PropertyInfos)
            {
                var property = propertyInfo.BuildProperty(propertyTypes[GetTypeName(propertyInfo.Type)]);
                properties.Add(property);
            }

            var category = new Category()
            {
                Name = data.CategoryName,
                Properties = properties
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            string GetTypeName(int id)
            {
                switch (id)
                {
                    case 0: return "Nominal";
                    case 1: return "Decimal";
                    case 2: return "Option";
                }

                return null;
            }
            
            return RedirectToAction("Products");
        }
        
        public IActionResult Products(string category, string query, int filterOption = 0)
        {
            var model = new AdminPanelProductsViewModel();
            model.Category = category;
            model.Query = query;
            model.FilterOption = filterOption;

            var categories = _dbContext.Categories.Select(c => new CategoryDTO()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            model.Categories = categories;

            var productsQuery = _dbContext.Products.Select(x => x);
            if (category != null && category != "all" && int.TryParse(category, out var categoryId))
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(query))
            {
                if (filterOption == 0)
                    productsQuery = productsQuery.Where(p => p.Name.ToLower().Contains(query.ToLower()));
                else if (filterOption == 1 && double.TryParse(query, out var ratingQuery))
                    productsQuery = productsQuery.Where(p => p.Rating == (decimal) ratingQuery);
                else if (filterOption == 2 && double.TryParse(query, out var priceQuery))
                    productsQuery = productsQuery.Where(p => p.Price == (decimal) priceQuery);
            }

            var products = productsQuery.Select(p => new AdminPanelProductDTO()
            {
                Id = p.Id,
                Name = p.Name,
                CategoryName = p.Category.Name,
                Price = (double)p.Price,
                Rating = (double)p.Rating
            }).ToList();

            model.Products = products;
            
            return View(model);
        }
        public IActionResult GetAdminMenu()
        {
            return PartialView("_GetAdminMenu");
        }
        
        [HttpGet]
        public IActionResult CreateProduct()
        {
            var model = new AdminPanelCreateProductViewModel();
            var categories = _dbContext.Categories.Select(c => new CategoryDTO()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            model.Categories = categories;
            
            return View(model);
        }

        [HttpGet("~/adminpanel/api/deleteProduct")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var imageData = _dbContext.Products.ImageById(productId).FirstOrDefault();

            if (imageData != null)
            {
                var imagePath = Path.Combine(_appEnvironment.WebRootPath, imageData.ImagePath);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }

            var product = _dbContext.Products.ById(productId).FirstOrDefault();
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.RemoveRange(_dbContext.Reviews
                    .Where(review => review.ProductId == product.Id)
                    .ToList());
            }

            if(imageData != null)
                _dbContext.ImageMetadata.Remove(imageData);
            
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(AdminPanelCreateProductViewModel data)
        {
            var categories = _dbContext.Categories.Select(c => new CategoryDTO()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            data.Categories = categories;

            if(data.Image == null)
                ModelState.AddModelError("", "Фото товара должно быть установлено");
            if (!ModelState.IsValid)
                return View(data);

            var product = new Product()
            {
                CategoryId = data.Category.Value,
                Name = data.ProductName,
                Description = data.ProductDescription,
                Price = (decimal) data.ProductPrice.Value,
                Rating = 0
            };

            var imageData = await CreateProductImageMetadata(data.Image);
            await _dbContext.ImageMetadata.AddAsync(imageData);

            await _dbContext.SaveChangesAsync();

            product.ImageId = imageData.Id;
            var attributeValues = new Dictionary<string, string>();

            foreach (var propertyInfo in data.PropertyInfos)
                attributeValues[propertyInfo.PropertyId.ToString()] = propertyInfo.Value;

            var jsonDoc = JsonDocument.Parse(JsonSerializer.SerializeToUtf8Bytes(attributeValues));
            product.AttributeValues = jsonDoc;

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            
            return RedirectToAction("Products");
        }

        public async Task<ImageMetadata> CreateProductImageMetadata(IFormFile imageFile)
        {
            var imageName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(imageFile.FileName);
            var virtualImagePath = Path.Combine("applicationData/productImages", imageName);
            var imagePath = Path.Combine(_appEnvironment.WebRootPath, virtualImagePath);

            await using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            var imageData = new ImageMetadata()
            {
                ImagePath = virtualImagePath,
                ContentType = imageFile.ContentType
            };

            return imageData;
        }

        [HttpGet("~/adminpanel/api/getproperties")]
        public IActionResult GetPropertyInfos(int categoryId)
        {
            if (!_dbContext.Categories.Any(c => c.Id == categoryId))
                return BadRequest();

            var properties = _dbContext.Categories
                .Where(c => c.Id == categoryId)
                .SelectMany(c => c.Properties)
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    PropertyType = p.Type.Name,
                    FilterInfo = p.FilterInfo.ToJsonString(),
                    Constraints = p.Constraints.ToJsonString()
                }).ToList();

            return Json(properties);
        }
        
        public IActionResult GetAllProductPictures()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var filePath in Directory.EnumerateFiles(Path.Combine(_appEnvironment.WebRootPath,
                        "applicationData/productImages")))
                    {
                        var file = archive.CreateEntry(Path.GetFileName(filePath));
                        using var streamWriter = file.Open();
                        using var fileReader = System.IO.File.OpenRead(filePath);
                            fileReader.CopyTo(streamWriter);
                    }
                }

                return File(memoryStream.ToArray(), "application/zip", "Images.zip");
            }
        }

        public IActionResult Cities()
        {
            var cities = _dbContext.Cities.Select(c=>c.Name).AsEnumerable();
            return View(cities);
        }
        public IActionResult DeleteCity(string cityName)
        {
            var city = _dbContext.Cities.FirstOrDefault(c => c.Name == cityName);
            if (city != null)
            {
                _dbContext.Cities.Remove(city);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Cities");
        }
        [HttpPost]
        public IActionResult AddCity(string cityName)
        {
            var cities = _dbContext.Cities.Select(c => c.Name).AsEnumerable();
            if (!cities.Contains(cityName))
            {
                _dbContext.Cities.Add(new City(cityName));
                _dbContext.SaveChanges();
            }
            else
            {
                ModelState.AddModelError(cityName, "Такой город уже существует");
            }
            return View("Cities",cities);
        }

        public IActionResult Shops()
        {
            var shops = _dbContext.Shops.Include(s=>s.City).ToList();
            var cities = _dbContext.Cities.Select(c => new SelectListItem(c.Name,c.Name)).AsEnumerable();
            var model = new ShopsViewModel()
            {
                Shops = shops,
                CityNames = cities,
            };
            return View(model);
        }

        public IActionResult AddShop(ShopsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var shop = new Shop()
                {
                    CityName = model.CityName,
                    Name = model.ShopName,
                    Address = model.ShopAddress,
                };
                if(_dbContext.Shops.FirstOrDefault(s => s.Address == shop.Address && s.Name == shop.Name && s.CityName == shop.CityName) == null)
                {
                    _dbContext.Shops.Add(shop);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Shops");
                }
                else
                {
                    ModelState.AddModelError(model.ShopName, "Такой магазин уже существует");
                }
                
            }
            var shops = _dbContext.Shops.Include(s => s.City).ToList();
            var cities = _dbContext.Cities.Select(c => new SelectListItem(c.Name, c.Name)).AsEnumerable();
            model.Shops = shops;
            model.CityNames = cities;
            return View("Shops", model);

        }

        public IActionResult DeleteShop(int shopId)
        {
            var shop = _dbContext.Shops.FirstOrDefault(s => s.Id == shopId);
            if (shop != null)
            {
                _dbContext.Remove(shop);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Shops");
        }
        public IActionResult Orders(AdminPanelOrdersViewModel model)
        {
            var orders = _dbContext.Orders.Where(x=>x==x);
            if (model.QueryId != 0)
            {
                if(model.StringSearchBy == ((int)SearchByEnum.Order).ToString())
                {
                    orders = orders.Where(o => o.Id == model.QueryId);
                }
                if (model.StringSearchBy == ((int)SearchByEnum.User).ToString())
                {
                    orders = orders.Where(o => o.UserId == model.QueryId);
                }
            }
            var ordersInMemory = orders.ToList();

            IOrderStates toHomeDeliveryOrderManager = new ToHomeDeliveryOrder();
            IOrderStates toShopDeliveryOrderManager = new ToShopDeliveryOrder();
            var ordersResult = ordersInMemory.Select(o => new AdminPanelOrderInfoViewModel()
            {
                OrderId = o.Id,
                OwnerId = o.UserId,
                CreateDate = o.CreateDate,
                OrderState = o.DeliveryMethod == DeliveryMethods.DeliveryToHome.GetString ?
                    new OrderState
                    {
                        State = o.State,
                        CssClass = toHomeDeliveryOrderManager.GetStateCssClass(o.State)
                    }
                    :
                    new OrderState
                    {
                        State = o.State,
                        CssClass = toShopDeliveryOrderManager.GetStateCssClass(o.State)
                    }

            });
           
            var newModel = new AdminPanelOrdersViewModel()
            
            {
                Orders = ordersResult,
                QueryId = model.QueryId == 0 ? 1 : model.QueryId,
                StringSearchBy = model.StringSearchBy,
                SearchBy = model.SearchBy==null ? new List<SelectListItem>() { 
                    new SelectListItem("Номеру заказа",((int)SearchByEnum.Order).ToString(),((int)SearchByEnum.Order).ToString()==model.StringSearchBy? true : false),
                    new SelectListItem("Идентификатору пользователя",((int)SearchByEnum.User).ToString(), ((int)SearchByEnum.User).ToString()==model.StringSearchBy? true : false)} : model.SearchBy

            };
            ModelState.Clear();
            TryValidateModel(newModel, nameof(newModel));
            return View(newModel);
        }

        public IActionResult RefreshFilter()
        {
            return RedirectToAction("Orders",new AdminPanelOrdersViewModel());
        }

        public IActionResult OrderPage(int orderId)
        {
            var order = _dbContext.Orders.Include(o=>o.OrderItems).FirstOrDefault(o => o.Id==orderId);
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == order.UserId);
            IOrderStates toHomeOrderStatesManager = new ToHomeDeliveryOrder();
            IOrderStates toShopOrderStatesManager = new ToShopDeliveryOrder();
            var orderStates = DeliveryMethods.DeliveryToHome.GetString == order.DeliveryMethod ?
                toHomeOrderStatesManager.GetAllStates() :
                toShopOrderStatesManager.GetAllStates();
            var selectList = orderStates.Select(o => new SelectListItem(o, o));
            var model = new AdminPanelOrderPageViewModel()
            {
                OrderId = orderId,
                OrderState = order.State,
                DeliveryMethod = order.DeliveryMethod,
                CreateDate = order.CreateDate,
                Address = order.Address,
                OrderItems = order.OrderItems,
                TotalPrice = order.TotalPrice,
                TotalCount = order.TotalCount,
                Email = user.Email,
                OrderStates = selectList,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> OrderPage(AdminPanelOrderPageViewModel model)
        {
            var order = _dbContext.Orders.Include(o=>o.OrderItems).FirstOrDefault(o => o.Id == model.OrderId);
            if(order.State!= model.OrderState)
            {
                order.State = model.OrderState;
                _dbContext.SaveChanges();
                var success = await _emailSender.SendEmailAsync(
                    model.Email,
                    "Смена статуса заказа",
                    string.Format("Ваш заказ номер {0} сменил статус на \"{1}\"", model.OrderId, model.OrderState));
                if (!success)
                {
                    IOrderStates toHomeOrderStatesManager = new ToHomeDeliveryOrder();
                    IOrderStates toShopOrderStatesManager = new ToShopDeliveryOrder();
                    var orderStates = DeliveryMethods.DeliveryToHome.GetString == order.DeliveryMethod ?
                        toHomeOrderStatesManager.GetAllStates() :
                        toShopOrderStatesManager.GetAllStates();
                    ModelState.AddModelError("", $"Уведомление о смене статуса заказа не может быть отправлено, т.к оно заблокированно по подозрению в спаме.\n");
                    var selectList = orderStates.Select(o => new SelectListItem(o, o));
                    model.OrderStates = selectList;
                    model.OrderItems = order.OrderItems;
                    model.Address = order.Address;
                    model.CreateDate = order.CreateDate;
                    model.TotalCount = order.TotalCount;
                    model.TotalPrice = order.TotalPrice;
                    return View("OrderPage", model);
                }
            }
            return RedirectToAction("Orders");
        }
    }
}