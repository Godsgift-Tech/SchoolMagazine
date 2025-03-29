using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.Email_Messaging
{
    public class EmailConfiguration
    {
        //public string From { get; set; } = null;
        //public string SmtpServer { get; set; } = null;
        //public int Port { get; set; }
        //public string UserName { get; set; } = null;
        //public string Password { get; set; } = null;
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromEmail { get; set; }
        public bool EnableSsl { get; set; }
    }
}
