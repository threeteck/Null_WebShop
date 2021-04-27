namespace WebShop_NULL.Models.ViewModels.СatalogModels
{
    public class ProductViewModel
    {
        public CategoryDTO Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath;
        public decimal Price { get; set; }
        public int Id { get; set; }
    }
}