using MovieRank.Contracts;
using MovieRank.Libs.Mappers;
using MovieRank.Libs.Repositories;
using System.Collections.Generic;
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

        public async Task AddMovie(int userId, MovieRankRequest movieRankRequest)
        {
            var movieDb = map.ToMovieDBModel(userId, movieRankRequest);
            await repository.AddMovie(movieDb);
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

        public async Task<IEnumerable<MovieResponse>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var response = await repository.GetUsersRankedMoviesByMovieTitle(userId, movieName);
            return map.ToMovieContract(response);
        }

        public async Task UpdateMovie(int userId, MovieUpdateRequest movieUpdateRequest)
        {
            var response = await repository.GetMovie(userId, movieUpdateRequest.movieName);

            var movieDb = map.ToMovieDBModel(userId, response, movieUpdateRequest);
            await repository.UpdateMovie(movieDb);
        }
    }
}
