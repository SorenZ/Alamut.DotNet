using Alamut.Data.Sql.EF.Repositories;
using Alamut.Data.Sql.EF.Test.Database;
using Xunit;

namespace Alamut.Data.Sql.EF.Test
{
    public class QueryRepositoryTestFixture
    {
        private readonly AppDbContext _context;

        public QueryRepositoryTestFixture()
        {
             _context = DbHelper.GetInMemoryInstance();
        }

        [Fact]
        public void Query_GetAll()         
        {
            // arrange 
            
            DbHelper.Seed(_context);

            var repository = new QueryRepository<Blog,int>(_context);

            // act
            var allItems = repository.GetAll();

            // var actual = 
            Assert.NotNull(allItems);
            Assert.NotEmpty(allItems);
        }
    }
}