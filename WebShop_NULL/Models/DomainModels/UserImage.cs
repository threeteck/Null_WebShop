namespace WebShop_NULL.Models.DomainModels
{
    public class UserImage
    {
        public virtual User User { get; set; }
        public byte[] Image { get; set; }
    }
}