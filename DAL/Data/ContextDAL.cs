using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL.Data;

public class ContextDAL(DbContextOptions<ContextDAL> options) : DbContext(options)
{
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
            .HasData([
                new User()
                {
                    Id = 1,
                    Name = "Itamar Herr",
                    Email = "itamar@gmail.com",
                    Password = "ItamarPassword1234!",
                    UserType = "Admin"
                },
                new User()
                {
                    Id = 2,
                    Name = "Ester Yonatan Herr",
                    Email = "ester@gmail.com",
                    Password = "EsterPassword1234!",
                    UserType = "User"
                },
                new User()
                {
                    Id = 3,
                    Name = "Ziv Herr",
                    Email = "ziv@gmail.com",
                    Password = "ZivPassword1234!",
                    UserType = "Guest"
                },

            ]);
        modelBuilder.Entity<Post>()
           .HasData([
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
           ]);
    }
}