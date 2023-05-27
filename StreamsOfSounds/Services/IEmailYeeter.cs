namespace StreamsOfSound.Services
{
    public interface IEmailYeeter
    {
        Task SignUpConfirmationAsync(string email, string opportunityName, string address, string city, string state, string zip, DateTimeOffset oppStartTime, DateTimeOffset oppEndTime, string instrumentName, DateTime startTime, DateTime endTime, string firstName, string lastName);
        Task IfYoureSeeingThisItsTooLate(string email, string opportunityName, string address, string city, string state, string zip, DateTimeOffset oppStartTime, DateTimeOffset oppEndTime);
}
}
