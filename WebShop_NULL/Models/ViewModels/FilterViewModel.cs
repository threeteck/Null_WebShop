namespace WebShop_NULL.Models.ViewModels
{
    public abstract class FilterViewModel
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public int PropertyType { get; set; }
    }
}