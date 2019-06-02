using System;
using Xunit;

namespace Alamut.Data.Sql.EF.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // arrange 
            var value1 = 2;
            var value2 = 3;
            var expected = 5;

            // act
            var actual = value1 + value2; 

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
