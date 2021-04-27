namespace WebShop_NULL.Models.ViewModels
{
    public class CategoryDTO//Moved to F# TODO test
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