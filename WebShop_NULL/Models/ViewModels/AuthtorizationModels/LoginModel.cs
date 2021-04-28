﻿using System.ComponentModel.DataAnnotations;

namespace WebShop_NULL.Models.AuthtorizationModels
{
    public class LoginModel//Moved to F# TODO test
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }
 
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public string RememberMe { get; set; }

    }
}