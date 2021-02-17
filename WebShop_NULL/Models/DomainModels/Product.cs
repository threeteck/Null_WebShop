using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace WebShop_NULL.Models.DomainModels
{
    public class Product
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ProductImage Image { get; set; }

        public JsonDocument AttributeValues { get; set; }

        public virtual ICollection<User> InBasketOf { get; set; }
    }
}