using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DAL.Data;

public class ContextDAL : IdentityDbContext<AppUser, IdentityRole<int>, int>
{
    public ContextDAL(DbContextOptions<ContextDAL> options) : base(options) { }
    public DbSet<Post> Posts { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var hasher = new PasswordHasher<AppUser>();

        modelBuilder.Entity<Post>()
       .HasOne(p => p.User)
       .WithMany()
       .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<IdentityRole<int>>().HasData(
            new IdentityRole<int>()
            {
                Id = 1,
                Name = "admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
        modelBuilder.Entity<AppUser>().HasData(
               new AppUser()
               {
                   Id = 1,
                   Name = "Itamar",
                   Email = "itamar@example.com",
                   NormalizedEmail = "ITAMAR@GMAIL.COM",
                   UserName = "itamar@example.com",
                   SecurityStamp = Guid.NewGuid().ToString(),
                   PasswordHash = hasher.HashPassword(null, "123456")
               },
        new AppUser
        {
            Id = 2,
            Name = "User2",
            Email = "user2@example.com",
            NormalizedEmail = "USER2@EXAMPLE.COM",
            UserName = "user2@example.com",
            NormalizedUserName = "USER2@EXAMPLE.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
            PasswordHash = hasher.HashPassword(null, "123456")
        },
        new AppUser
        {
            Id = 3,
            Name = "User3",
            Email = "user3@example.com",
            NormalizedEmail = "USER3@EXAMPLE.COM",
            UserName = "user3@example.com",
            NormalizedUserName = "USER3@EXAMPLE.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
            PasswordHash = hasher.HashPassword(null, "123456")
        }
           );

        modelBuilder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int>()
            {
                RoleId = 1,
                UserId = 1
            }
        );

        modelBuilder.Entity<Post>()
          
           .HasData(
               new Post()
               {
                   Id = 1,
                   Content = "Can planting more trees help offset carbon emissions?",
                   PostDate = new DateTime(2024, 10, 10, 21, 45, 0),
                   UserId = 2,
                   ParentPostId = null
               },
               new Post()
               {
                   Id = 2,
                   Content = "How do wetlands contribute to ecological health?",
                   PostDate = new DateTime(2024, 10, 10, 21, 47, 0),
                   UserId = 3,
                   ParentPostId = null
               },
               new Post()
               {
                   Id = 3,
                   Content = "Wetlands improve ecological health by supporting biodiversity, filtering water, reducing floods, storing carbon, and protecting shorelines.",
                   PostDate = new DateTime(2024, 10, 10, 21, 47, 0),
                   UserId = 1,
                   ParentPostId = 2,
               }
           );
    }
}