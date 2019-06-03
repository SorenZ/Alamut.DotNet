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

        public static AppDbContext GetInMemoryInstance(bool populated = true)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("test")
                .Options;



            var context =  new AppDbContext(options);

            return context;
        }

        public static void Seed(AppDbContext context)
        {
            context.Blogs.Add(new Blog
            {
                Url = "https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory",
                Rating = 5
            });

            context.SaveChanges();
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }


}