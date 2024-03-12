using Microsoft.EntityFrameworkCore;
using Blog.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using System;
using System.Linq;


namespace Blog
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users {get; set;}
        public DbSet<Article> Articles { get; set;}
        public DbSet<Comment> Comments { get; set;}
        public DbSet<Tag> Tags { get; set;}
        public DbSet<Role> Roles { get; set;}

        public DataContext(DbContextOptions<DataContext> options)  : base(options)
        {   
            Database.EnsureCreated();

            if (Roles == null)
            {
                Roles.Add(new Role
                {
                    Id = 1,
                    Name = "Пользователь"
                });
                Roles.Add(new Role
                {
                    Id = 2,
                    Name = "Администратор"
                });
                Roles.Add(new Role
                {
                    Id = 3,
                    Name = "Модератор"
                });
            }
        }
    }
}