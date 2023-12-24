using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Owin.Testing;
using OwinWebApi;
using Xunit;

namespace OwinWebApiTest
{
    public class HomeTest
    {
        [Fact]
        public async Task HomeTestAsync()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var response = await server.HttpClient.GetAsync("/");
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                responseString.Should().Be("\"API Works\"");
            }
        }
    }
}