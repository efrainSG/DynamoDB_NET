using MovieRank.Integration.Tests.Setup;
using MovieRank.Libs.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieRank.Integration.Tests.Scenarios {
    [Collection("api")]
    public class MovieTests : IClassFixture<CustomWebApplicationFactory<Startup>> {
        readonly HttpClient client;
        public MovieTests(CustomWebApplicationFactory<Startup> factory) {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task AddMovieRankDataReturnsOkStatus() {
            const int userId = 1;

            var movieDbData = new MovieDb {
                UserId = userId,
                MovieName = "Test movie name",
                Description = "test description movie",
                Actors = new List<string> {
                    "actor 1",
                    "actor 2"
                },
                RankDateTime = DateTime.UtcNow.ToString(),
                Ranking = 5
            };

            var json = JsonConvert.SerializeObject(movieDbData);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"movies/{userId}", stringContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
