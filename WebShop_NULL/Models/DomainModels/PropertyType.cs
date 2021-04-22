using System.ComponentModel.DataAnnotations;

namespace WebShop_NULL.Models.DomainModels
{
    public enum PropertyTypeEnum
    {
        Integer = 0,
        Decimal = 1,
        Option = 2,
        Nominal = 3
    }
    
    public class PropertyType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}