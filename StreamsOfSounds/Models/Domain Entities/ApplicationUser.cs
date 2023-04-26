using Microsoft.AspNetCore.Identity;

namespace StreamsOfSound.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Instruments { get; set; } 
        public string? Position { get; set; }    
        public bool? Archive { get; set; }
    }
}
