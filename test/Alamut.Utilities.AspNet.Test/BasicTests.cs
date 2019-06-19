using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Alamut.Utilites.AspNet.Test
{
    public class BasicTests
    {
        private readonly WebApplicationFactory<Alamut.Utilities.AspNet.Sut.Startup> _sut;

        public BasicTests()
        {
            _sut = new WebApplicationFactory<Alamut.Utilities.AspNet.Sut.Startup>();
        }

        [Fact]
        public async Task BasicTest()
        {
            // arrange 
            var client = _sut.CreateDefaultClient();


            // act
            var response = await client.GetAsync("/");

            // assert 
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", 
            response.Content.Headers.ContentType.ToString());
        }
    }
}
