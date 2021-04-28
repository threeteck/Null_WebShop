using System.Collections.Generic;

namespace WebShop_NULL.Models.ViewModels.AdminPanelModels
{
    public class AdminPanelProductsViewModel//Moved to F# TODO test
    {
        public string Category = null;
        public string Query = null;
        public int FilterOption = 0;

        public IEnumerable<CategoryDTO> Categories;
        public IEnumerable<AdminPanelProductDTO> Products;
    }
}