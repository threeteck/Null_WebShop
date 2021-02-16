using System.ComponentModel.DataAnnotations;

namespace WebShop_NULL.Models.DomainModels
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}