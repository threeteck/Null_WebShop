using System;

namespace WebShop_NULL.Infrastructure.Filters
{
    public class ForPropertyAttribute : Attribute
    {
        public int PropertyId;

        public ForPropertyAttribute(int propertyId)
        {
            PropertyId = propertyId;
        }
    }
}