/*using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace BarCodeTester
{
    public class EmailTester
    {
        public void EmailSender(MemoryStream stream)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("hymatikorders@gmail.com");
                mail.To.Add("hymatikorders@gmail.com");
                mail.Subject = "Test Mail3";
                mail.Body = "This is for testing SMTP mail from GMAIL";
                Attachment att = new Attachment(stream, "report.pdf", "application/pdf");
                mail.Attachments.Add(att);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("hymatikorders@gmail.com", "marketing123.");
                SmtpServer.EnableSsl = true;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.UseDefaultCredentials = false;

                SmtpServer.Send(mail);

                Console.WriteLine("mail Send");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }

}
