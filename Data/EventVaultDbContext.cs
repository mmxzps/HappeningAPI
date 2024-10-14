using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventVault.Models;

namespace EventVault.Data
{
    public class EventVaultDbContext : IdentityDbContext<IdentityUser>
    {
        public EventVaultDbContext(DbContextOptions options) : base(options)
        {
        }

        //DbSets

        DbSet<Event> Events { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //add modelbuilders for entity to prevent cascading delete or hasdata for database.
        }
    }
}
