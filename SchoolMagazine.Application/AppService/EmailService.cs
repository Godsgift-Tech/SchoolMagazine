using Microsoft.Extensions.Configuration;
using SchoolMagazine.Application.AppInterface;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Your Name or Display Name", _configuration
                    ["EmailSettings:FromEmail"]));
                emailMessage.To.Add(new MailboxAddress("Recipient Name or Display Name", to));
                emailMessage.Subject = subject;

                var bodyBuilder = new BodyBuilder { HtmlBody = body };
                emailMessage.Body = bodyBuilder.ToMessageBody();

                using (var smtpClient = new SmtpClient())
                {
                    await smtpClient.ConnectAsync(_configuration["EmailSettings:Host"],
                        int.Parse(_configuration["EmailSettings:Port"]), true);
                    await smtpClient.AuthenticateAsync(_configuration["EmailSettings:UserName"],
                     _configuration["EmailSettings:Password"]);
                    await smtpClient.SendAsync(emailMessage);
                    Console.WriteLine("Email sent successfully!");
                    await smtpClient.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                throw new InvalidOperationException("An unexpected error " +
                    "occurred while sending the email.", ex);
            }
        }

        // Initiating Email Template 
        public async Task<string> GetEmailTemplate(string templateFileName, string confirmationLink,
            string userName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", templateFileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Email template not found at: {filePath}");

            var template = await File.ReadAllTextAsync(filePath);

            return template.Replace("{UserName}", userName)
                           .Replace("{ConfirmationLink}", confirmationLink);
        }
    }

}
