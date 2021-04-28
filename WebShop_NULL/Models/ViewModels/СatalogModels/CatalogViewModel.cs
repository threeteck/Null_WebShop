using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModels;

namespace WebShop_NULL.Models.ViewModels.СatalogModels
{
    public class CatalogViewModel//Moved to F# TODO test
    {
        public CategoryDTO Category { get; set; }
        public int Page { get; set; }
        public int NumberOfPages { get; set; }
        public int SortingOption { get; set; }
        public ICollection<CategoryDTO> Categories { get; set; }
        public List<ProductCardDTO> ProductList { get; set; }
    }
}
