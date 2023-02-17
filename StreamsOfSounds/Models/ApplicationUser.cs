using Microsoft.AspNetCore.Identity;

namespace StreamsOfSounds.Models
{
    //Application user is essentially going to be the Volunteers  
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}
