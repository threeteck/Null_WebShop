using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebShop_NULL.Infrastructure.Filters
{
    public class FilterModelBinder : IModelBinder
    {
        private readonly FilterMapper<FilterDTO> _filterMapper;

        public FilterModelBinder(FilterMapper<FilterDTO> filterMapper)
        {
            _filterMapper = filterMapper;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if(bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var modelName = bindingContext.ModelName;
            var propertyIdValue = bindingContext.ValueProvider.GetValue($"{modelName}.PropertyId");
            var propertyTypeValue = bindingContext.ValueProvider.GetValue($"{modelName}.PropertyType");
            if (propertyIdValue == ValueProviderResult.None ||
                propertyTypeValue == ValueProviderResult.None ||
                !int.TryParse(propertyIdValue.FirstValue, out var propertyId) ||
                !int.TryParse(propertyTypeValue.FirstValue, out var propertyType))
                return Task.CompletedTask;

            Type filterType = null;
            if (_filterMapper.ContainsId(propertyId))
                filterType = _filterMapper.GetFilterForProperty(propertyId);
            else if (_filterMapper.ContainsType(propertyType))
                filterType = _filterMapper.GetFilterForType(propertyType);
            else return Task.CompletedTask;

            var metadata = _filterMapper.GetFilterPropertiesMetadata(filterType);
            var obj = _filterMapper.GetFilterFactory(filterType)();
            
            foreach (var property in metadata)
            {
                //TODO: Try to improve with fallback to default model binder
                var propertyName = property.Name;
                var resolvedType = property.UnderlyingOrModelType;
                var value = bindingContext.ValueProvider.GetValue($"{modelName}.{propertyName}");
                var firstValue = value.FirstValue;
                if (firstValue != null && !string.IsNullOrWhiteSpace(firstValue))
                {
                    if (!property.IsCollectionType)
                        property.PropertySetter(obj, Convert.ChangeType(firstValue, resolvedType));
                    else
                    {
                        var elementType = property.ElementType;
                        property.PropertySetter(obj, value.Values.ToList());
                    }
                }

            }
            
            bindingContext.Result = ModelBindingResult.Success((FilterDTO)obj);
            return Task.CompletedTask;
        }
    }
}