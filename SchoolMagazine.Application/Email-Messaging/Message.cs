using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.Email_Messaging
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("email", x)));
            Subject = subject;
            Content = content;
        }

    }
    //public class Message
    //{
    //    public List<MailboxAddress> To { get; set; }
    //    public string Subject { get; set; }
    //    public string Content { get; set; }

    //    public Message(IEnumerable<string> to, string subject, string content)
    //    {
    //        if (to == null || !to.Any() || to.Any(email => string.IsNullOrWhiteSpace(email)))
    //            throw new ArgumentException("Recipient email addresses cannot be null or empty.");

    //        To = to.Select(email => new MailboxAddress(email, email)).ToList();
    //        Subject = subject;
    //        Content = content;
    //    }
    //}

}
