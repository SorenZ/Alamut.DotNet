using System;
using Xunit;
using Alamut.Data.Structure;

namespace Alamut.Data.Test
{
    public class ServiceResultTest
    {
        [Fact]
        public void ServiceResult_Equals_In_Same_Objects()
        {
            // arrange
             var actual = ServiceResult.Okay("Alamut is great.");
             var expected = ServiceResult.Okay("Alamut is great.");

            // assert
            Assert.Equal(expected,actual);
                
        }

        [Fact]
        public void ServiceResult_Not_Equals_In_Difference_Objects()
        {
            // arrange
             var actual = ServiceResult.Okay("Alamut is great.");
             var expected = ServiceResult.Error("Alamut is great.");

            // assert
            Assert.NotEqual(expected,actual);
                
        }
         [Fact]
        public void ServiceResult_Generic_Equals_In_Same_Objects()
        {
            // arrange
             var actual = ServiceResult<Foo>.Okay(new Foo { Bar = "Alamut is great."});
             var expected =ServiceResult<Foo>.Okay(new Foo { Bar = "Alamut is great."});

            // assert
            Assert.Equal(expected,actual);
                
        }

        class Foo : IEquatable<Foo>
        {
            public string Bar { get; set; }

            public override bool Equals(object obj)
            {
                var sr = obj as Foo;
                if(sr == null) {return false;}

                return sr.Bar.Equals(this.Bar);
            }

            public override int GetHashCode() => this.Bar.GetHashCode();

            public bool Equals(Foo other) 
            {
                throw new NotImplementedException();
                // Console.WriteLine("Equals(Foo other)");
                // return this.Bar.Equals(other.Bar);
            }
        }
    }
}
