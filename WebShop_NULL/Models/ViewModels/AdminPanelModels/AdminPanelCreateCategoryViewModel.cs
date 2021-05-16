using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using DomainModels;
using Microsoft.AspNetCore.Mvc;
using WebShop_NULL.Infrastructure.AdminPanel;

namespace WebShop_NULL.Models.ViewModels.AdminPanelModels
{
    [ModelBinder(BinderType = typeof(CreateCategoryPropertyInfoBinder))]
    public class CreateCategoryPropertyInfo
    {
        public string Name { get; set; }
        public int Type { get; set; }

        public virtual Property BuildProperty(PropertyType type)
        {
            var property = new Property()
            {
                Name = Name,
                Type = type,
                FilterInfo = JsonDocument.Parse("{}"),
                Constraints = JsonDocument.Parse("{}")
            };

            return property;
        }
    }

    public class CreateCategoryOptionPropertyInfo : CreateCategoryPropertyInfo
    {
        public List<string> Options { get; set; }

        public override Property BuildProperty(PropertyType type)
        {
            var property = base.BuildProperty(type);

            var obj = new
            {
                options = Options
            };

            var json = JsonSerializer.SerializeToUtf8Bytes(obj);
            var jDoc = JsonDocument.Parse(json);

            property.FilterInfo = jDoc;
            return property;
        }
    }

    public class CreateCategoryNumberPropertyInfo : CreateCategoryPropertyInfo
    {
        public double MinValue;
        public double MaxValue;

        public override Property BuildProperty(PropertyType type)
        {
            var property = base.BuildProperty(type);

            var obj = new
            {
                minValue = MinValue,
                maxValue = MaxValue
            };
            
            var json = JsonSerializer.SerializeToUtf8Bytes(obj);
            var jDoc = JsonDocument.Parse(json);

            property.Constraints = jDoc;
            return property;
        }
    }

    public class AdminPanelCreateCategoryViewModel
    {
        [Required(ErrorMessage = "Имя категории должно быть указано")]
        public string CategoryName { get; set; }

        public List<CreateCategoryPropertyInfo> PropertyInfos { get; set; }
    }
}