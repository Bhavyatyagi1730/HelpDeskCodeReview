using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;

namespace Service.Class
{
    public class Email : IEmail
    {
       
        public void SendEmail(string to, string subject, string body)
        {
            string smtpServer = "smtp.gmail.com";
            string smtpUsername = "abhigautam.5678@gmail.com";
            string smtpPassword = "gntbbqrhuphfichj";
            int smtpPort = 587;
           
          


            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

            MailMessage message = new MailMessage("abhigautam.5678@gmail.com", "abhigautam.5678@gmail.com");
            //message.To.Add(to);
            message.Subject = subject;
            message.Body = body;

            smtpClient.Send(message);



        }
    }
}

