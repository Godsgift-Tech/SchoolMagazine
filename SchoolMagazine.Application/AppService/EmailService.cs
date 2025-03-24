using MimeKit;
using MailKit.Net.Smtp;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.Email_Messaging;
using System;
using System.Linq;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;

namespace SchoolMagazine.Application.AppService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(Message message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Your App Name", _configuration["EmailSettings:From"]));
            email.To.AddRange(message.To);
            email.Subject = message.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = message.Content };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration["EmailSettings:SmtpServer"],
                                    int.Parse(_configuration["EmailSettings:Port"]),
                                    SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration["EmailSettings:Username"],
                                         _configuration["EmailSettings:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }


}
