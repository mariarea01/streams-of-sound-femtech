using Microsoft.AspNetCore.Identity.UI.Services;

namespace StreamsOfSounds.Services
{
    public class EmailSender : IEmailSender
    {
        Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine(email);
            return Task.CompletedTask;
        }
    }
}
