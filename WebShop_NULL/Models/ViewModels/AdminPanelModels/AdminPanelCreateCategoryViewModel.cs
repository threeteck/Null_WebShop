using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebShop_NULL.Infrastructure.AdminPanel;

namespace WebShop_NULL.Models.ViewModels.AdminPanelModels
{
    [ModelBinder(BinderType = typeof(CreateCategoryPropertyInfoBinder))]
    public class CreateCategoryPropertyInfo
    {
        public string Name { get; set; }
        public int Type { get; set; }
    }

    public class CreateCategoryOptionPropertyInfo : CreateCategoryPropertyInfo
    {
        public List<string> Options { get; set; }
    }

    public class AdminPanelCreateCategoryViewModel
    {
        [Required(ErrorMessage = "Имя категории должно быть указано")]
        public string CategoryName { get; set; }

        public List<CreateCategoryPropertyInfo> PropertyInfos { get; set; }
    }
}