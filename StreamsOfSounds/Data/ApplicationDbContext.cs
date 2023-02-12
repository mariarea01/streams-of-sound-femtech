using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using StreamsOfSounds.Models.Domain_Entities;
using StreamsOfSounds.Models;


namespace StreamsOfSounds.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Opportunity> Opportunities { get; set; }

        public DbSet<StreamsOfSounds.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}