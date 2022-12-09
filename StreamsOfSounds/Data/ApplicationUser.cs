using Microsoft.AspNetCore.Identity;

namespace StreamsOfSounds.Data
{
    //Application user is essentially going to be the Volunteers  
    public class ApplicationUser : IdentityUser 
    {
        public string FirstName { get; set; }   

        public string LastName { get; set; } 
    }
}
