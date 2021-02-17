using System.ComponentModel.DataAnnotations;

namespace WebShop_NULL.Models.DomainModels
{
    public class UserImage
    {
        [Key]
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public byte[] Image { get; set; }
    }
}