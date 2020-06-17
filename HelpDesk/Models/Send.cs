using EASendMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Models
{
    public class Send
    {
        public async Task Send_Email(Ticket ticket)
        {
            String Result = "";
            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");
                SmtpClient oSmtp = new SmtpClient();

                // Set sender email address, please change it to yours
                oMail.From = new MailAddress("ghadialghosh@outlook.com");
                // Add recipient email address, please change it to yours
                oMail.To.Add(new MailAddress("ghadialghosh@outlook.com"));

                // Set email subject and email body text
                oMail.Subject = "Welcome to HelpDesk, Your Ticket Number" +" "+ ticket.Id;
                oMail.TextBody = "this is a test email sent from HelpDesk App, do not reply";

                // Your SMTP server address
                SmtpServer oServer = new SmtpServer("smtp.office365.com");

                // User and password for SMTP authentication
                oServer.User = "Youremail@outlook.com";
                oServer.Password = "YourPassword";

                // If your SMTP server requires TLS connection on 25 port, please add this line
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                // If your SMTP server requires SSL connection on 465 port, please add this line
                // oServer.Port = 465;
                // oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                await oSmtp.SendMailAsync(oServer, oMail);
                Result = "Ticket#"+ ticket.Id + "  " + "created successfully & email sent to Customer!";
            }
            catch (Exception ep)
            {
                Result = String.Format("Failed to send email with the following error: {0}", ep.Message);
            }

            // Display Result by Diaglog box
            Windows.UI.Popups.MessageDialog dlg = new
                Windows.UI.Popups.MessageDialog(Result);

            await dlg.ShowAsync();
        }
    }
}
