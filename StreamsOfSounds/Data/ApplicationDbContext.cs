using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StreamsOfSounds.Models;
using VolunteerWebApplication.Models;

namespace StreamsOfSounds.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Opportunities> Opportunities { get; set; }
    }
}
