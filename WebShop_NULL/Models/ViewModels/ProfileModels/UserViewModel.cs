using System.ComponentModel.DataAnnotations;

namespace WebShop_NULL.Models.ViewModels
{
    public class UserViewModel//Moved to F# TODO test
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