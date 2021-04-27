using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Required] 
        public int? Category { get; set; } = null;
        [Required]
        [MaxLength(64)]
        public string ProductName { get; set; }
        [Required]
        [MaxLength(1024)]
        public string ProductDescription { get; set; }
        [Required]
        public double? ProductPrice { get; set; } = null;

        public IEnumerable<CategoryDTO> Categories;

        public IFormFile Image { get; set; }
        public List<CreateProductPropertyInfo> PropertyInfos { get; set; }
    }
}