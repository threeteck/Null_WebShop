using System;
using DomainModels;

namespace WebShop_NULL.Infrastructure.Filters
{
    public class ForPropertyTypeAttribute : Attribute
    {
        public PropertyTypeEnum Type;

        public ForPropertyTypeAttribute(PropertyTypeEnum type)
        {
            Type = type;
        }
    }
}