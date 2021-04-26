namespace WebShop_NULL.Models.ViewModels
{
    public class CategoryDTO
    {
        public int Id;
        public string Name;
        
        public CategoryDTO(){}

        public CategoryDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}