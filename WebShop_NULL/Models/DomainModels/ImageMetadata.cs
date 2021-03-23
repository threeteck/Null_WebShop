using System.ComponentModel.DataAnnotations;

namespace WebShop_NULL.Models.DomainModels
{
    public class ImageMetadata
    {
        [Key]
        public int Id { get; set; }
        
        public string ImagePath { get; set; }
        public string ContentType { get; set; }
    }
}