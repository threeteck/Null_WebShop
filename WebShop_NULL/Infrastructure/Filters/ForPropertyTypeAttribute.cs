using System;

namespace WebShop_NULL.Infrastructure.Filters
{
    public class ForPropertyTypeAttribute : Attribute
    {
        public int Type;

        public ForPropertyTypeAttribute(int type)
        {
            Type = type;
        }
    }
}