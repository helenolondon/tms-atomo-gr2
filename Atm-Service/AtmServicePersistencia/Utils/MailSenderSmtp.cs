using System.Net;
using System.Net.Mail;

namespace AtmServiceInfra.Utils
{
    public class MailSenderSmtp : IMailSender
    {
        public void SendEmail(string recipient, string body, string subject, bool isBodyHtml = false)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            var mailMessage = new MailMessage();


            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("heleno.sales.mesquita@gmail.com", "bdwxqpdzyuemrphq");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            mailMessage.From = new MailAddress("heleno.sales.mesquita@gmail.com");
            mailMessage.Subject = subject;
            mailMessage.To.Add(new MailAddress(recipient));

            mailMessage.Body = body;
            mailMessage.IsBodyHtml = isBodyHtml;

            smtpClient.Send(mailMessage);
        }
    }
}
