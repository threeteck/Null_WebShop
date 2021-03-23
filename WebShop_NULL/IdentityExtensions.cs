using System.Security.Claims;
using WebShop_NULL.Models.DomainModels;

namespace WebShop_NULL
{
    public static class IdentityExtensions
    {
        public static int GetId(this ClaimsPrincipal principal)
        {
            if (principal.Identity.IsAuthenticated)
            {
                var id = principal.FindFirst("id")?.Value;
                if (id != null && int.TryParse(id, out var result))
                    return result;
            }

            return -1;
        }
        
        public static string GetName(this ClaimsPrincipal principal)
        {
            if (principal.Identity.IsAuthenticated)
            {
                var id = principal.FindFirst("name")?.Value;
                return id;
            }

            return null;
        }
    }
}