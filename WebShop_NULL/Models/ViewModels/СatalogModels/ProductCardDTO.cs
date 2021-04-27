namespace WebShop_NULL.Models.ViewModels.СatalogModels
{
    public class ProductCardDTO
    {
        public int Id;
        public string Name;
        public decimal Price;
        public string ImagePath;
        public decimal Rating { get; set; }
    }
}