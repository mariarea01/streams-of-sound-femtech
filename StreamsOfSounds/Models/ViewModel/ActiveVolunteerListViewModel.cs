namespace StreamsOfSound.Models.ViewModel
{
    public class ActiveVolunteerListViewModel : ApplicationUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Instruments { get; set; }
    }
}
