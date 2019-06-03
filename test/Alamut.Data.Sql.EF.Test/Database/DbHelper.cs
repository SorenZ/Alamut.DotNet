using Microsoft.EntityFrameworkCore;

namespace Alamut.Data.Sql.EF.Test.Database
{
    public static class DbHelper
    {
        public static AppDbContext GetInMemoryInstance()
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
    }

}