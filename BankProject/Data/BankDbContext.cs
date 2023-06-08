using BankProject.Models.Domain;

using Microsoft.EntityFrameworkCore;

namespace BankProject.Data
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }


    }
}
