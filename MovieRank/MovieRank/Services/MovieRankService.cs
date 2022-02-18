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

        public async Task<IEnumerable<MovieResponse>> getUsersRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var response = await repository.getUsersRankedMoviesByMovieTitle(userId, movieName);
            return map.ToMovieContract(response);
        }
    }
}
