using Alamut.Data.Sql.EF.Repositories;
using Alamut.Data.Sql.EF.Test.Database;
using Xunit;

namespace Alamut.Data.Sql.EF.Test
{
    public class QueryRepositoryTestFixture
    {
        [Fact]
        public void Query_GetAll()         
        {
            // arrange 
            var context = AppDbContext.GetInMemoryInstance(true);
            AppDbContext.Seed(context);

            var repository = new QueryRepository<Blog,int>(context);

            // act
            var allItems = repository.GetAll();

            // var actual = 
            Assert.NotNull(allItems);
            Assert.NotEmpty(allItems);
        }
    }
}