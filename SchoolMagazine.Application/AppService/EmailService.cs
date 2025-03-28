//using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.Email_Messaging;
using System.Net;
using System.Net.Mail;
//using System.Net.Mail;

namespace SchoolMagazine.Application.AppService
{





    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587; //  Correct Port
                    smtpClient.Credentials = new NetworkCredential("godsgiftoghenechohwo@gmail.com", "lxgk rznf mchp fayh");
                    smtpClient.EnableSsl = true; //  Use TLS

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("godsgiftoghenechohwo@gmail.com"),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(toEmail);

                    await smtpClient.SendMailAsync(mailMessage);
                    Console.WriteLine("Email sent successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }


}
