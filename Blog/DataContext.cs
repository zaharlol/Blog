using Microsoft.EntityFrameworkCore;
using Blog.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using System;


namespace Blog
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users {get; set;}
        public DbSet<Article> Articles { get; set;}
        public DbSet<Comment> Comments { get; set;}
        public DbSet<Tag> Tags { get; set;}

        public DataContext(DbContextOptions<DataContext> options)  : base(options)
        {
            Database.EnsureCreated();
        }
    }
}