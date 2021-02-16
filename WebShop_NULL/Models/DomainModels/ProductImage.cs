using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop_NULL.Models.DomainModels
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public byte[] Image { get; set; }
    }
}