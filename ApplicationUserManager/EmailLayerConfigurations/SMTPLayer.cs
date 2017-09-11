using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace UserAppService.EmailLayerConfigurations
{
    public class SMTPLayer
    {
        public async Task configSmtpasync(IdentityMessage message)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;

            try
            {
                mailMessage.Subject = message.Subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = message.Body;
                mailMessage.To.Add(message.Destination);

                await smtpClient.SendMailAsync(mailMessage);

                msg = "Successful<BR>";
            }

            catch( Exception ex )
            {
                msg = ex.Message;
            }
        }
    }
}
