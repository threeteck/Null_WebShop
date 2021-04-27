using System.Threading.Tasks;

namespace WebShop_NULL
{
    public interface IEmailSender
    {
        public Task<bool> SendEmailAsync(string email, string subject, string message);
    }
}    
