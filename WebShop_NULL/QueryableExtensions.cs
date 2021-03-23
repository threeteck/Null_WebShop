using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebShop_NULL.Models.DomainModels;

namespace WebShop_NULL
{
    public static class QueryableExtensions
    {
        public static IQueryable<User> ById(this IQueryable<User> userSet, int id)
        {
            return userSet.Where(user => user.Id == id);
        }
        
        public static IQueryable<ImageMetadata> ImageById(this IQueryable<User> userSet, int id)
        {
            return userSet.ById(id).Select(user => user.Image);
        } 
    }
}