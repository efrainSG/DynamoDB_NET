﻿using Microsoft.AspNetCore.Mvc;
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
    }
}
