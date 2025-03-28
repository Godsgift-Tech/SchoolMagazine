using SchoolMagazine.Application.Email_Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppInterface
{
    public interface IEmailService
    {
        //void SendEmail(Message message);
        //Task<bool> SendEmailAsync(string to, string subject, string body);
        //Task SendEmailAsync(string toEmail, string subject, string message);
        //void SendEmail(string to, string subject, string body);
        Task SendEmailAsync(string to, string subject, string body);
        //  Task SendEmailAsync(string toEmail, string subject, string message);
        //Task SendEmailAsync(Message message);
    }
}
