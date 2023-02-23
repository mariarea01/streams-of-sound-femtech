using Microsoft.AspNetCore.Identity;

namespace StreamsOfSound.Models.Domain_Entities
{
    public partial class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}