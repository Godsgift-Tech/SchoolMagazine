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
        Task SendEmailAsync(string to, string subject, string body);
        Task<string> GetEmailTemplate(string templateFileName, string confirmationLink, string userName);
        Task SendJobAlertEmailAsync(string toEmail, string subject, string htmlMessage);
        //Task<string> GetJobAlertTemplate(string templateFileName, string jobTitle,
        //    string location, string qualification, string description, string postedAt);


        Task<string> GetJobAlertTemplate(
     string templateFileName,
     string jobTitle,
     string location,
     string qualification,
     string categories,
     string minSalary,
     string maxSalary,
     string? description,
     string postedAt);

    }
}
