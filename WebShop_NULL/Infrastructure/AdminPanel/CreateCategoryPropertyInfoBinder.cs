using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebShop_NULL.Models.ViewModels.AdminPanelModels;

namespace WebShop_NULL.Infrastructure.AdminPanel
{
    public class CreateCategoryPropertyInfoBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if(bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var modelName = bindingContext.ModelName;
            var propertyNameResult = bindingContext.ValueProvider.GetValue($"{modelName}.Name");
            var propertyTypeResult = bindingContext.ValueProvider.GetValue($"{modelName}.Type");
            if (propertyNameResult == ValueProviderResult.None ||
                propertyTypeResult == ValueProviderResult.None ||
                !int.TryParse(propertyTypeResult.FirstValue, out var propertyType) ||
                string.IsNullOrWhiteSpace(propertyNameResult.FirstValue) ||
                propertyType < 0 || propertyType > 2)
            {
                ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            if (propertyType == 2)
            {
                var propertyOptionsResult = bindingContext.ValueProvider.GetValue($"{modelName}.Options");
                if (propertyOptionsResult == ValueProviderResult.None)
                {
                    ModelBindingResult.Failed();
                    return Task.CompletedTask;
                }
                    
                var options = propertyOptionsResult.Values.ToList();
                bindingContext.Result = ModelBindingResult.Success(new CreateCategoryOptionPropertyInfo()
                {
                    Name = propertyNameResult.FirstValue,
                    Type = propertyType,
                    Options = options
                });
            
                return Task.CompletedTask;
            }
            
            bindingContext.Result = ModelBindingResult.Success(new CreateCategoryPropertyInfo()
            {
                Name = propertyNameResult.FirstValue,
                Type = propertyType
            });
            
            return Task.CompletedTask;
        }
    }
}