using Microsoft.AspNetCore.Mvc;
using MovieRank.Contracts;
using MovieRank.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Controllers
{
    [Route("movies")]
    public class MovieController : Controller
    {
        private readonly IMovieRankService movieRankService;

        public MovieController(IMovieRankService movieRankService)
        {
            this.movieRankService = movieRankService;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieResponse>> getAllItemsFromDatabase()
        {
            var results = await movieRankService.GetAllItemsFromDatabase();
            return results;
        }

        [HttpGet, Route("{userId}/{movieName}")]
        public async Task<MovieResponse> getMovie(int userId, string movieName)
        {
            var result = await movieRankService.GetMovie(userId, movieName);
            return result;
        }

        [HttpGet, Route("user/{userId}/rankedMovies/{movieName}")]
        public async Task<IEnumerable<MovieResponse>> getUsersRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var result = await movieRankService.GetUsersRankedMoviesByMovieTitle(userId, movieName);
            return result;
        }

        [HttpPost, Route("{userId}")]
        public async Task<IActionResult> addMovie(int userId, [FromBody] MovieRankRequest movieRankRequest)
        {
            await movieRankService.AddMovie(userId, movieRankRequest);

            return Ok();
        }

        [HttpPatch, Route("{userId}")]
        public async Task<IActionResult> UpdateMovie(int userId, [FromBody] MovieUpdateRequest movieUpdateRequest)
        {
            await movieRankService.UpdateMovie(userId, movieUpdateRequest);
            return Ok();
        }

        [HttpGet, Route("{movieName}/ranking")]
        public async Task<MovieRankResponse> GetMoviesRanking(string movieName)
        {
            var result = await movieRankService.GetMoviesRanking(movieName);

            return result;
        }
    }
}
