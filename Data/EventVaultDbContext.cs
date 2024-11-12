using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventVault.Models;
using System.Reflection.Emit;

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
            builder.Entity<Event>()
                .HasOne(e => e.Venue)             
                .WithMany(v => v.Events)           
                .HasForeignKey(e => e.FK_Venue)    
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete: deleting a Venue deletes all Events at venue

            builder.Entity<Event>()
                .Property(e => e.HighestPrice)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Event>()
                .Property(e => e.LowestPrice)
                .HasColumnType("decimal(18,2)");

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
