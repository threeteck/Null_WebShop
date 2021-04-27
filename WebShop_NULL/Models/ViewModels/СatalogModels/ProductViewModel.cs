using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
//Moved to F# TODO test
namespace WebShop_NULL.Models.ViewModels.СatalogModels
{
    public class ProductViewModel
    {
        public CategoryDTO Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath;
        public decimal Price { get; set; }
        public int Id { get; set; }
        public decimal Rating { get; set; }

        public IEnumerable<PropertyDTO> Properties;
        public IEnumerable<ReviewDTO> Reviews;
    }
}