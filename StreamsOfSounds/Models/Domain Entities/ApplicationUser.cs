using Microsoft.AspNetCore.Identity;

namespace StreamsOfSounds.Models
{
    //Application user is essentially going to be the Volunteers  
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
