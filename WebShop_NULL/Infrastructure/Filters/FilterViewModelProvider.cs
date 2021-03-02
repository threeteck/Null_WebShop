using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebShop_NULL.Models.ViewModels;

namespace WebShop_NULL.Infrastructure.Filters
{
    public class FilterViewModelProvider
    {
        private readonly FilterMapper<FilterViewModel> _mapper;
        private readonly Dictionary<Type, IFilterViewModelBuilder<FilterViewModel>> _buildersMap;
        private IModelMetadataProvider _provider;

        public FilterViewModelProvider(FilterMapper<FilterViewModel> mapper, IModelMetadataProvider provider)
        {
            _mapper = mapper;
            _provider = provider;
            _buildersMap = new Dictionary<Type, IFilterViewModelBuilder<FilterViewModel>>();
            Initialize();
        }

        private void Initialize()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            foreach (var type in types.Where(t =>
                !t.IsInterface && t
                    .GetInterfaces()
                    .Any(i =>i.IsGenericType && i.GetGenericTypeDefinition()
                              == typeof(IFilterViewModelBuilder<>))))
            {
                var obj = (IFilterViewModelBuilder<FilterViewModel>) Activator.CreateInstance(type);
                var implementingType = type.GetInterfaces()
                    .Single(i => i.IsGenericType && i.GetGenericTypeDefinition()
                        == typeof(IFilterViewModelBuilder<>))
                    .GenericTypeArguments[0];

                _buildersMap[implementingType] = obj;
            }
        }

        public FilterViewModel GetFilterViewModel(string propertyName, int propertyType, int propertyId, dynamic filterInfo)
        {
            Type filterViewType = null;
            if (_mapper.ContainsId(propertyId))
                filterViewType = _mapper.GetFilterForProperty(propertyId);
            else if (_mapper.ContainsType(propertyType))
                filterViewType = _mapper.GetFilterForType(propertyType);
            else return null;

            var filterViewModel = (FilterViewModel)_mapper.GetFilterFactory(filterViewType)();
            filterViewModel.PropertyId = propertyId;
            filterViewModel.PropertyType = propertyType;
            filterViewModel.PropertyName = propertyName;
            
            if (_buildersMap.ContainsKey(filterViewType))
                filterViewModel = _buildersMap[filterViewType]
                    .BuildFilterViewModel(filterViewModel, filterInfo);
            /*else
            {
                var metadata = _mapper.GetFilterPropertiesMetadata(filterViewType);
                foreach (var property in metadata)
                {
                    if(filterInfo.RootElement.TryGetProperty(property.Name, out var p))
                        p.
                }
            }*/

            return filterViewModel;
        }
    }
}