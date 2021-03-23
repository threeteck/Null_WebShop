using System.ComponentModel.DataAnnotations;

namespace WebShop_NULL.Models.DomainModels
{
    public class ImageMetadata
    {
        public static ImageMetadata DefaultImage => new ImageMetadata()
        {
            ImagePath = "applicationData/profileImages/default.jpg",
            ContentType = "image/jpeg"
        };
        
        [Key]
        public int Id { get; set; }
        
        public string ImagePath { get; set; }
        public string ContentType { get; set; }
    }
}