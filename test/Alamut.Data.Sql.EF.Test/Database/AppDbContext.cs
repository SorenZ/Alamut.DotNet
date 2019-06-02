using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Alamut.Data.Sql.EF.Test.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        { }

        public AppDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        
    }
}