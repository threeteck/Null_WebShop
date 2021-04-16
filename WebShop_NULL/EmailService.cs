using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp; 
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using WebShop_FSharp;

namespace WebShop_NULL
{
    public class EmailService:IEmailSender
    {
        private EmailSettings _settings;
        private ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> settings, ILogger<EmailService> logger)
        {
            _logger = logger;
            _settings = settings.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
 
            emailMessage.From.Add(new MailboxAddress("NULL_WebShop", _settings.Username));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("Plain")
            {
                Text = message
            };

            _logger.LogInformation($"message: {message}");
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_settings.Address, _settings.Port, _settings.UseSSL);
                    await client.AuthenticateAsync(_settings.Username, _settings.Password);
                    await client.SendAsync(emailMessage);
                    
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"message was not sent. Reason: {e.Message}");
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}