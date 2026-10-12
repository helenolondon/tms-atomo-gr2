using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceInfra.Utils
{
    public interface IMailSender
    {
        public void SendEmail(string recipient, string body, string subject, bool isBodyHtml = false);
    }
}
