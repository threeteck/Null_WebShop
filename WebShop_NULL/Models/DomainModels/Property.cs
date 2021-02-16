using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace WebShop_NULL.Models.DomainModels
{
    public class Property
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual PropertyType Type { get; set; }
        public JsonDocument Constraints { get; set; }
        public JsonDocument FilterInfo { get; set; }
    }
}