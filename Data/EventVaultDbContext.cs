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

            //database setup with existing users

            var adminRoleId = "fa4a730a-c49c-4675-a9d6-f30cc6c0f626";
            var userRoleId = "df5fef41-8ab5-41f3-8fe6-33f7a1d8c732";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = null
                },
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = null
                }
            );

            var defaultPassword = Environment.GetEnvironmentVariable("DefaultPassword");
            var hasher = new PasswordHasher<User>();

            var adminUserId = "user-admin-id";
            var amandaUserId = "user-amanda-id";
            var fredrichUserId = "user-fredrich-id";
            var mojtabaUserId = "user-mojtaba-id";
            var seanUserId = "user-sean-id";
            var stinaUserId = "user-stina-Id";

            builder.Entity<User>().HasData(
                new User
                {
                    Id = adminUserId,
                    UserName = "AdminAdam",
                    NormalizedUserName = "ADMINADAM",
                    Email = "adam.eventvault@gmail.com",
                    NormalizedEmail = "ADAM.EVENTVAULT@GMAIL.COM",
                    PhoneNumber = "0730717171",
                    FirstName = "Adam",
                    LastName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "Password123¤%&"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new User
                {
                    Id = amandaUserId,
                    UserName = "Amanda",
                    NormalizedUserName = "AMANDA",
                    Email = "amanda.olving@chasacademy.se",
                    NormalizedEmail = "AMANDA.OLVING@CHASACADEMY.SE",
                    PhoneNumber = "0730727272",
                    FirstName = "Amanda",
                    LastName = "Olving",
                    PasswordHash = hasher.HashPassword(null, "Password123¤%&"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new User
                {
                    Id = fredrichUserId, // Fredrich User ID
                    UserName = "Fredrich",
                    NormalizedUserName = "FREDRICH",
                    Email = "fredrich.benedetti@chasacademy.se",
                    NormalizedEmail = "FREDRICH.BENEDETTI@CHASACADEMY.SE",
                    PhoneNumber = "0730737373",
                    FirstName = "Fredrich",
                    LastName = "Benedetti",
                    PasswordHash = hasher.HashPassword(null, "Password123¤%&"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new User
                {
                    Id = mojtabaUserId, // Mojtaba User ID
                    UserName = "Mojtaba",
                    NormalizedUserName = "MOJTABA",
                    Email = "mojtaba.mobasheri@chasacademy.se",
                    NormalizedEmail = "MOJTABA.MOBASHERI@CHASACADEMY.SE",
                    PhoneNumber = "0730747474",
                    FirstName = "Mojtaba",
                    LastName = "Mobasheri",
                    PasswordHash = hasher.HashPassword(null, "Password123¤%&"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new User
                {
                    Id = seanUserId, // Sean User ID
                    UserName = "Sean",
                    NormalizedUserName = "SEAN",
                    Email = "sean-harry.ortega@chasacademy.se",
                    NormalizedEmail = "SEAN-HARRY.ORTEGA@CHASACADEMY.SE",
                    PhoneNumber = "0730757575",
                    FirstName = "Sean",
                    LastName = "Ortega Schelin",
                    PasswordHash = hasher.HashPassword(null, "Password123¤%&"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new User
                {
                    Id = stinaUserId, // Stina User ID
                    UserName = "Stina",
                    NormalizedUserName = "STINA",
                    Email = "stina.hedman@chasacademy.se",
                    NormalizedEmail = "STINA.HEDMAN@CHASACADEMY.SE",
                    PhoneNumber = "0730767676",
                    FirstName = "Stina",
                    LastName = "Hedman",
                    PasswordHash = hasher.HashPassword(null, "Password123¤%&"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUserId, // Admin User ID
                    RoleId = "fa4a730a-c49c-4675-a9d6-f30cc6c0f626"  // Admin Role ID
                },
                new IdentityUserRole<string>
                {
                    UserId = amandaUserId, // Amanda User ID
                    RoleId = "df5fef41-8ab5-41f3-8fe6-33f7a1d8c732"  // User Role ID
                },
                new IdentityUserRole<string>
                {
                    UserId = fredrichUserId, // Fredrich User ID
                    RoleId = "df5fef41-8ab5-41f3-8fe6-33f7a1d8c732"  // User Role ID
                },
                new IdentityUserRole<string>
                {
                    UserId = mojtabaUserId, // Mojtaba User ID
                    RoleId = "df5fef41-8ab5-41f3-8fe6-33f7a1d8c732"  // User Role ID
                },
                new IdentityUserRole<string>
                {
                    UserId = seanUserId, // Mojtaba User ID
                    RoleId = "df5fef41-8ab5-41f3-8fe6-33f7a1d8c732"  // User Role ID
                },
                new IdentityUserRole<string>
                {
                    UserId = stinaUserId, // Mojtaba User ID
                    RoleId = "df5fef41-8ab5-41f3-8fe6-33f7a1d8c732"  // User Role ID
                }

            );

        }
    }
}
