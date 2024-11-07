using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventVault.Models;
using System.Reflection.Emit;

namespace EventVault.Data
{
    public class EventVaultDbContext : IdentityDbContext<IdentityUser>
    {
        public EventVaultDbContext(DbContextOptions<EventVaultDbContext> options) : base(options)
        {
        }

        //DbSets

        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Event>()
            .HasOne(e => e.Venue)             
            .WithMany(v => v.Events)           
            .HasForeignKey(e => e.FK_Venue)    
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete: deleting a Venue deletes all Events at venue

            base.OnModelCreating(builder);

            //add modelbuilders for entity to prevent cascading delete or hasdata for database.
        }
    }
}
