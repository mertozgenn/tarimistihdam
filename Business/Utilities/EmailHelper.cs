using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities
{
    public class EmailHelper
    {
        public static void SendMail(EmailMessage emailMessage)
        {
            try
            {
                SmtpClient sc = new SmtpClient();
                sc.Port = emailMessage.Parameters.SmtpPort;
                sc.Host = emailMessage.Parameters.SmtpServer;
                sc.EnableSsl = emailMessage.Parameters.IsSSL;
                sc.Credentials = new NetworkCredential(emailMessage.Parameters.Email, emailMessage.Parameters.EmailPassword);

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(emailMessage.Parameters.Email, emailMessage.Parameters.DisplayName);

                mail.To.Add(emailMessage.Receiver);

                mail.Subject = emailMessage.Subject;
                mail.IsBodyHtml = true;
                mail.Body = emailMessage.Body;
                sc.Send(mail);
            }
            catch (Exception)
            {
            }
        }
    }
}
