﻿using System.Collections.Generic;
using DomainModels;

namespace WebShop_NULL.Models.ViewModels.СatalogModels
{
    public class ProductsViewModel
    {
        public List<Product> ProductList { get; set; }
        public string Category { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}