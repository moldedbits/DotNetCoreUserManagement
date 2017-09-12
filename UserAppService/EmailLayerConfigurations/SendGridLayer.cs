//using SendGrid.Helpers.Mail;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using UserAppService.Models;
//using UserAppService.Utility.Extensions;

//namespace UserAppService.EmailLayerConfigurations
//{
//    public class SendGridLayer
//    {
//        public async Task configSendGridasync(IdentityMessageExtended message)
//        {
//            var myMessage = new SendGridMessage();

//            //Need to ensure that when email address is not present on a user profile we are not attempting to send an email.
//            if( message.Destination.HasNoValue() )
//            {
//                return;
//            }

//            myMessage.AddTo(message.Destination);
//            myMessage.From = new EmailAddress("no-reply@moldedbits.com", "MoldedBits");
//            myMessage.Subject = message.Subject;


//            myMessage.PlainTextContent = message.Body;
//            myMessage.HtmlContent = typeof(IdentityMessageExtended).Equals(message.GetType()) ? ((IdentityMessageExtended) message).Html : message.Body;


//            var credentials = new NetworkCredential(
//                       ConfigurationManager.AppSettings [ "mailAccount" ],
//                       ConfigurationManager.AppSettings [ "mailPassword" ]);

//            //Create a Web transport for sending email.

//            //Will be implemented when AZURE will come into picture

//            //var transportWeb = new SendGrid.Web(credentials);
//            //try
//            //{
//            //    // Send the email.
//            //    if( transportWeb != null )
//            //    {
//            //        await transportWeb.DeliverAsync(myMessage);
//            //    }
//            //    else
//            //    {
//            //        Trace.TraceError("Failed to create Web transport.");
//            //        await Task.FromResult(0);
//            //    }
//            //}
//            //catch( Exception ex )
//            //{
//            //    Trace.TraceError(ex.Message + " SendGrid probably not configured correctly.");
//            //}
//        }
//    }
//}
