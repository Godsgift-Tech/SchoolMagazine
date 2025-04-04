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
                // Create a MimeMessage
                var emailMessage = new MimeMessage();

                // Set the sender's email (From)
                emailMessage.From.Add(new MailboxAddress("Your Name or Display Name", _configuration["EmailSettings:FromEmail"]));

                // Set the recipient's email (To)
                emailMessage.To.Add(new MailboxAddress("Recipient Name or Display Name", to));

                // Set the subject of the email
                emailMessage.Subject = subject;

                // Create the email body with HTML content
                var bodyBuilder = new BodyBuilder { HtmlBody = body };
                emailMessage.Body = bodyBuilder.ToMessageBody();

                // Connect to SMTP server using MailKit SmtpClient
                using (var smtpClient = new SmtpClient())
                {
                    // Ensure the client connects and authenticates
                    await smtpClient.ConnectAsync(_configuration["EmailSettings:Host"], int.Parse(_configuration["EmailSettings:Port"]), true);

                    // Authenticate using credentials from configuration
                    await smtpClient.AuthenticateAsync(_configuration["EmailSettings:UserName"], _configuration["EmailSettings:Password"]);

                    // Send the email asynchronously
                    await smtpClient.SendAsync(emailMessage);
                    Console.WriteLine("Email sent successfully!");

                    // Disconnect and dispose the client after sending the email
                    await smtpClient.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                // Log the exception for further debugging
                throw new InvalidOperationException("An unexpected error occurred while sending the email.", ex);
            }
        }
    }
}
