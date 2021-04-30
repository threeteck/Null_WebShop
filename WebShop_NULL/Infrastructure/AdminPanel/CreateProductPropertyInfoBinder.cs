using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebShop_NULL.Models.ViewModels.AdminPanelModels;

namespace WebShop_NULL.Infrastructure.AdminPanel
{
    public class CreateProductPropertyInfoBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if(bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var modelName = bindingContext.ModelName;
            var propertyIdResult = bindingContext.ValueProvider.GetValue($"{modelName}.PropertyId");
            var propertyValueResult = bindingContext.ValueProvider.GetValue($"{modelName}.Value");
            if (propertyIdResult == ValueProviderResult.None ||
                propertyValueResult == ValueProviderResult.None ||
                !int.TryParse(propertyIdResult.FirstValue, out var propertyId) ||
                propertyValueResult.FirstValue == null)
            {
                ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(new CreateProductPropertyInfo()
            {
                PropertyId = propertyId,
                Value = propertyValueResult.FirstValue
            });
            
            return Task.CompletedTask;
        }
    }
}