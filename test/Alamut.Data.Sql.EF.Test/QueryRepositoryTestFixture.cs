using Alamut.Data.Sql.EF.Repositories;
using Alamut.Data.Sql.EF.Test.Database;
using Xunit;

namespace Alamut.Data.Sql.EF.Test
{
    public class QueryRepositoryTests
    {
        private readonly AppDbContext _context;

        public QueryRepositoryTests()
        {
             _context = DbHelper.GetInMemoryInstance();
        }

        [Fact]
        public void GetById()
        {
            // arrange
            var expected = DbHelper.Seed_SignleBlog(_context);
            var repository = new QueryRepository<Blog,int>(_context);

            // act
            var actual = repository.GetById(expected.Id);

            // assert
            Assert.Equal(expected, actual);

        }


        [Fact]
        public void GetAll()         
        {
            // arrange 
            DbHelper.Seed_SignleBlog(_context);
            var repository = new QueryRepository<Blog,int>(_context);

            // act
            var allItems = repository.GetAll();

            // var actual = 
            Assert.NotNull(allItems);
            Assert.NotEmpty(allItems);
        }
    }
}