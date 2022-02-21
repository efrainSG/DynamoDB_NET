using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using MovieRank.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Libs.Repositories
{
    public interface IMovieRankRepository
    {
        Task<ScanResponse> GetAllItems();
        Task<GetItemResponse> GetMovie(int userId, string movieName);
        Task<QueryResponse> GetUsersRankedMoviesByMovieTitle(int userId, string movieName);
        Task AddMovie(int userId, MovieRankRequest movieRankRequest);
        Task UpdateMovie(int userId, MovieUpdateRequest updateRequest);
        Task<QueryResponse> GetMoviesRanking(string movieName);
        Task CreateDynamoDbTable(string dynamoDbtableName);
    }
}
 