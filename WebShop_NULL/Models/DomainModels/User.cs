using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShop_NULL.Models.DomainModels
{
    public enum UserRoleEnum
    {
        Normal = 0,
        Admin = 1
    }
    
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }
        public string HashedPassword { get; set; }
        
        public virtual UserImage Image { get; set; }
        public virtual UserRole Role { get; set; }
        public decimal TotalPayment { get; set; }
        
        public virtual ICollection<Product> Basket { get; set; }
    }
}