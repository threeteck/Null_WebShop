namespace WebShop_NULL.Models.ViewModels.СatalogModels
{
    public class ProductCardDTO//Moved to F# TODO test
    {
        public int Id;
        public string Name;
        public decimal Price;
        public string ImagePath;
        public decimal Rating { get; set; }
    }
}