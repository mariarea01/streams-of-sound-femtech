using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using System.Net;
using System.Net.Mail;

namespace StreamsOfSounds.Services
{
    public class EmailSender : IEmailSender
    {
        Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            //client.Port = 587;

            //client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network; 
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("seca8303@gmail.com", "ttkzxocysojgkfii");

            var message = new MailMessage();
            message.Subject = subject;
            message.To.Add(email);
            message.From = new MailAddress("seca8303@gmail.com");
            message.Body = htmlMessage; 
            message.IsBodyHtml = true;  

            client.Send(message);

            return Task.CompletedTask;
        }
    }
}
