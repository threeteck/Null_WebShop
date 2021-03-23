using System.ComponentModel.DataAnnotations;

namespace WebShop_NULL.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id;
        [MaxLength(16)]
        [Required]
        public string Name { get; set; }
        [MaxLength(16)]
        [Required]
        public string Surname { get; set; }

        public string Email;
    }
}