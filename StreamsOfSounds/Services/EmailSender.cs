using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace StreamsOfSounds.Services
{
    public class EmailSender : IEmailSender
    {
        Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.Port = 587;

            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network; 
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("seca8303@gmail.com", "ttkzxocysojgkfii");
            client.Send("seca8303@gmail.com", email, subject, htmlMessage);

            return Task.CompletedTask;
        }
    }
}
