using System.Threading.Tasks;

namespace WebShop_NULL
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message);
    }
}    