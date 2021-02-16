namespace WebShop_NULL.Models.DomainModels
{
    public class ProductImage
    {
        public virtual Product Product { get; set; }
        public byte[] Image { get; set; }
    }
}