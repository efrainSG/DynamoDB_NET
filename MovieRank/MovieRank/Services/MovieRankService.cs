using MovieRank.Contracts;
using MovieRank.Libs.Mappers;
using MovieRank.Libs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRank.Services
{
    public class MovieRankService : IMovieRankService
    {
        private readonly IMovieRankRepository repository;
        private readonly IMapper map;

        public MovieRankService(IMovieRankRepository repository, IMapper map)
        {
            this.repository = repository;
            this.map = map;
        }

        public async Task<IEnumerable<MovieResponse>> GetAllItemsFromDatabase()
        {
            var response = await repository.GetAllItems();

            return map.ToMovieContract(response);
        }

        public async Task<MovieResponse> GetMovie(int userId, string movieName)
        {
            var response = await repository.GetMovie(userId, movieName);
            return map.ToMovieContract(response);
        }

        public async Task<MovieRankResponse> GetMoviesRanking(string movieName)
        {
            var response = await repository.GetMoviesRanking(movieName);
            var overallMovieRanking = Math.Round(response.Items.Select(x => Convert.ToInt32(x["Ranking"].N)).Average());
            return new MovieRankResponse
            {
                MovieName = movieName,
                OverallRanking = overallMovieRanking
            };
        }

        public async Task<IEnumerable<MovieResponse>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var response = await repository.GetUsersRankedMoviesByMovieTitle(userId, movieName);
            return map.ToMovieContract(response);
        }

        public async Task AddMovie(int userId, MovieRankRequest movieRankRequest)
        {
            await repository.AddMovie(userId, movieRankRequest);
        }

        public async Task UpdateMovie(int userId, MovieUpdateRequest movieUpdateRequest)
        {
            await repository.UpdateMovie(userId, movieUpdateRequest);
        }


    }
}
