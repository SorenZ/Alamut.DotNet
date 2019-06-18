using Alamut.Data.Sql.EF.Repositories;
using Alamut.Data.Sql.EF.Test.Database;
using Alamut.Data.SSOT;
using Alamut.Data.Structure;
using Xunit;

namespace Alamut.Data.Sql.EF.Test
{
    public class RepositoryTest
    {
        private readonly AppDbContext _context;
        public RepositoryTest()
        {
            _context = DbHelper.GetInMemoryInstance();
        }

        [Fact]
        public void Repository_Create_Test()
        {
            // arrange
            var repository = new Repository<Blog,int>(_context);
            var entity = new Blog
            {
                Url = "https://github.com/SorenZ/Alamut.DotNet",
                Rating = 5
            };
            var expected = ServiceResult<int>.Okay(1, Messages.ItemCreated);


            // act
            var actual = repository.Create(entity);

            // assert
            Assert.Equal(expected.Data, actual.Data);

        }

        [Fact]
        public void Repository_AddRange()
        {
            // arrange
            var repository = new Repository<Blog,int>(_context);
            var entities = new []
            {
                new Blog
            {
                Url = "https://github.com/SorenZ/Alamut.DotNet",
                Rating = 5
            },
                new Blog
            {
                Url = "https://github.com/SorenZ/DataFramework",
                Rating = 4
            }
            };
            var expected = ServiceResult.Okay(Messages.ItemsCreated);


            // act
            var actual = repository.AddRange(entities,commit:true);

            // assert
            Assert.Equal(expected, actual);

        }
    }
}