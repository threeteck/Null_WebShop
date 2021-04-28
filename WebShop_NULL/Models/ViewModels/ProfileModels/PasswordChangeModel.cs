using System.ComponentModel.DataAnnotations;

namespace WebShop_NULL.Models.ViewModels
{
    public class PasswordChangeModel//Moved to F# TODO test
    {
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
 
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}