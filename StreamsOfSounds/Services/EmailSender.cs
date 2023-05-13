using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Net;

namespace StreamsOfSounds.Services
{
    public class EmailSender : IEmailSender
    {

        Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);

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

        //    private readonly string _apiKey = "";

        //    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        //    {
        //        var client = new SendGridClient(_apiKey);
        //        var from = new EmailAddress("no-reply@streamsofsoundinc.onmicrosoft.com", "StreamsOfSound");
        //        var to = new EmailAddress(email);
        //        var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlMessage);

        //        await client.SendEmailAsync(msg);
        //    }

        //    public async Task SignUpConfirmationAsync(string email, string opportunityName, string address, DateTimeOffset oppStartTime, DateTimeOffset oppEndTime, string instrumentName, DateTime startTime, DateTime endTime, string firstName, string lastName)
        //    {
        //        var subject = $"Opportunity Details - {opportunityName}";
        //        var body = $"Hello {firstName} {lastName},<br><br>" +
        //                   $"You have signed up for an opportunity named {opportunityName} that will take place at {address} from {oppStartTime.ToLocalTime():dddd, dd MMMM yyyy} to {oppEndTime.ToLocalTime():dddd, dd MMMM yyyy} to.<br><br>" +
        //                   $"The instrument you will be playing is {instrumentName}.<br><br>" +
        //                   $"The instrument time slot starts at {startTime.ToLocalTime().ToString("HH:mm")} and ends at {endTime.ToLocalTime().ToString("HH:mm")}.<br><br>" +
        //                   $"Thank you for signing up!";
        //        IEmailSender emailSender = new EmailSender();
        //        await emailSender.SendEmailAsync(email, subject, body);
        //    }
        //}
    }
}


