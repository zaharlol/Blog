using Microsoft.EntityFrameworkCore;
using Blog.Models;
using Microsoft.AspNetCore.Connections;


namespace Blog
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users {get; set;}

        public DataContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Blog.db");
        }
    }
}