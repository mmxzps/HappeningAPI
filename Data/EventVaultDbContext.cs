using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventVault.Models;

namespace EventVault.Data
{
    public class EventVaultDbContext : IdentityDbContext<User>
    {
        public EventVaultDbContext(DbContextOptions<EventVaultDbContext> options) : base(options)
        {
        }

        //DbSets

        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<User>  Users {  get; set; }
        public DbSet<Friendship> Friendships { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Friendship>()
                .HasOne(f => f.User)
                .WithMany(u => u.Friendships)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friendship>()
                .HasOne(f => f.Friend)
                .WithMany()
                .HasForeignKey(f=>f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
