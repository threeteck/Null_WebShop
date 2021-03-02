using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebShop_NULL.Models.DomainModels;

namespace WebShop_NULL.Infrastructure.Filters
{
    public class FilterMapper<T>
    {
        private readonly Dictionary<int, Type> _filterTypeMap;
        private readonly Dictionary<int, Type> _filterIdMap;
        private readonly Dictionary<Type, Func<object>> _factories;
        private readonly Dictionary<Type, ModelMetadata> _filterMetadata;
        private readonly Dictionary<Type, List<ModelMetadata>> _propertiesMetadata;

        private readonly IModelMetadataProvider _metadataProvider;
        

        public FilterMapper(IModelMetadataProvider metadataProvider)
        {
            _metadataProvider = metadataProvider;
            _filterIdMap = new Dictionary<int, Type>();
            _filterTypeMap = new Dictionary<int, Type>();
            _factories = new Dictionary<Type, Func<object>>();
            _filterMetadata = new Dictionary<Type, ModelMetadata>();
            _propertiesMetadata = new Dictionary<Type, List<ModelMetadata>>();
            Initialize();
        }

        private void Initialize()
        {  
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            var filteredTypes = types.Where(t => t.IsSubclassOf(typeof(T)));
            if (typeof(T).IsInterface)
                filteredTypes = types.Where(t => !t.IsInterface &&
                                                 t.GetInterfaces().Any(i => i == typeof(T)));

            foreach (var type in filteredTypes)
            {
                if (type.GetCustomAttribute<ForPropertyTypeAttribute>() is var forTypeAttribute &&
                    forTypeAttribute != null)
                    _filterTypeMap[forTypeAttribute.Type] = type;
                
                if (type.GetCustomAttribute<ForPropertyAttribute>() is var forIdAttribute &&
                    forIdAttribute != null)
                    _filterIdMap[forIdAttribute.PropertyId] = type;

                var ctor = type.GetConstructor(new Type[] { });
                if(ctor == null)
                    throw new Exception($"Type '{type}' does not have parameterless constructor");
                _factories[type] = (Func<object>)Expression.Lambda(Expression.New(ctor)).Compile();

                _filterMetadata[type] = _metadataProvider.GetMetadataForType(type);
                _propertiesMetadata[type] = _metadataProvider.GetMetadataForProperties(type).ToList();
            }
        }

        public bool ContainsType(int typeId)
            => _filterTypeMap.ContainsKey(typeId);

        public bool ContainsType(PropertyTypeEnum type)
            => ContainsType((int) type);

        public bool ContainsId(int id)
            => _filterIdMap.ContainsKey(id);

        public Type GetFilterForType(int typeId)
        {
            if (!ContainsType(typeId))
                return null;

            return _filterTypeMap[typeId];
        }

        public Type GetFilterForType(PropertyTypeEnum type)
            => GetFilterForType((int) type);
        
        public Type GetFilterForProperty(int propertyId)
        {
            if (!ContainsId(propertyId))
                return null;

            return _filterIdMap[propertyId];
        }

        public Func<object> GetFilterFactory(Type type)
            => _factories[type];

        public ModelMetadata GetFilterMetadata(Type type)
            => _filterMetadata[type];

        public IEnumerable<ModelMetadata> GetFilterPropertiesMetadata(Type type)
            => _propertiesMetadata[type];
    }
}