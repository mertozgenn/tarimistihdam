using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities
{
    public class EmailMessage
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Receiver { get; set; }
        public string? CC { get; set; }
        public EmailParameter Parameters { get; set; }
    }

    public class EmailParameter
    {
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool IsSSL { get; set; }
        public string DisplayName { get; set; }
    }
}
