using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop_FSharp.ViewModels;
using WebShop_NULL.Infrastructure.AdminPanel;

namespace WebShop_NULL.Models.ViewModels.AdminPanelModels
{
    [ModelBinder(BinderType = typeof(CreateProductPropertyInfoBinder))]
    public class CreateProductPropertyInfo
    {
        public int PropertyId { get; set; }
        public string Value { get; set; }
    }
    
    public class AdminPanelCreateProductViewModel
    {
        [Required(ErrorMessage = "Категория должна быть указана")] 
        public int? Category { get; set; } = null;
        [Required(ErrorMessage = "Имя товара должно быть указано")]
        [MaxLength(64)]
        public string ProductName { get; set; }
        [MaxLength(1024)]
        public string ProductDescription { get; set; }
        [Required(ErrorMessage = "Цена товара должна быть указана")]
        public double? ProductPrice { get; set; } = null;

        public IEnumerable<CategoryDTO> Categories;

        public IFormFile Image { get; set; }
        public List<CreateProductPropertyInfo> PropertyInfos { get; set; }
    }
}