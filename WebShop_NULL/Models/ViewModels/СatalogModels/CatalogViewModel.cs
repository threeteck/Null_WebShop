using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModels;

namespace WebShop_NULL.Models.ViewModels.СatalogModels
{
    public class CatalogViewModel
    {
        public string Category { get; set; }
        public ICollection<string> Categories { get; set; }
        public List<Product> ProductList { get; set; }
    }
}
