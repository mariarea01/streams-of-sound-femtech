using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;
using StreamsOfSound.Services;

namespace StreamsOfSounds.Services
{
    public class EmailSender : IEmailSender, IEmailYeeter
    {
         async Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp-relay.sendinblue.com", 587);

            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("sos.noreplyemail@gmail.com", "0BXhpbZHKcV59sDR");

            //client.Credentials = new NetworkCredential("sos.noreplyemail@gmail.com", "dksricremelijdxd");

            var message = new MailMessage();
            message.Subject = subject;
            message.To.Add(email);
            message.From = new MailAddress("sos.noreplyemail@gmail.com");
            message.Body = htmlMessage;
            message.IsBodyHtml = true;

            client.Send(message);

            return;
        }

        public async Task SignUpConfirmationAsync(string email, string opportunityName, string address, string city, string state, string zip, DateTimeOffset oppStartTime, DateTimeOffset oppEndTime, string instrumentName, DateTime startTime, DateTime endTime, string firstName, string lastName)
        {
            var subject = $"Opportunity Details - {opportunityName}";
            var body = $"Hello {firstName} {lastName},<br><br>" +
                       $"You have signed up for an opportunity named {opportunityName} that will take place at {address} from {oppStartTime.ToLocalTime():dddd, dd MMMM yyyy} to {oppEndTime.ToLocalTime():dddd, dd MMMM yyyy} to.<br><br>" +
                       $"The instrument you will be playing is {instrumentName}.<br><br>" +
                       $"The instrument time slot starts at {startTime.ToLocalTime().ToString("HH:mm")} and ends at {endTime.ToLocalTime().ToString("HH:mm")}.<br><br>" +
                       $"Thank you for signing up!";
            IEmailSender emailSender = new EmailSender();
            await emailSender.SendEmailAsync(email, subject, body);
        }

        public async Task IfYoureSeeingThisItsTooLate(string email, string opportunityName, string address, string city, string state, string zip, DateTimeOffset oppStartTime, DateTimeOffset oppEndTime)
        {
            var subject = $"New Opportunity Posted - {opportunityName}";
            var body = $"Good Day Volunteer,<br><br>" +
                       $"A new Opportunity has been posted. View the details below." +
                       $"{opportunityName} that will take place at {address} from {oppStartTime.ToLocalTime():dddd, dd MMMM yyyy} to {oppEndTime.ToLocalTime():dddd, dd MMMM yyyy}<br><br>" +
                       $"If you would like to sign up, please log into your Streams of Sound Account to view Instrument Slots.";
            IEmailSender emailSender = new EmailSender();
            await emailSender.SendEmailAsync(email, subject, body);
        }


    }
}



