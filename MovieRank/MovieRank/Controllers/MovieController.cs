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
    }
}
