using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieRank.Libs.Models;

namespace MovieRank.Libs.Repositories
{
    public class MovieRankRepositoryAsDocument : IMovieRankRepositoryAsDocument
    {
        private readonly Table table;

        public MovieRankRepositoryAsDocument(IAmazonDynamoDB dynamoDbClient)
        {
            table = Table.LoadTable(dynamoDbClient, Constants.TableName);
        }

        public async Task AddMovie(Document documentModel)
        {
            await table.PutItemAsync(documentModel);
        }

        public async Task<IEnumerable<Document>> GetAllItems()
        {
            var config = new ScanOperationConfig();

            return await table.Scan(config).GetRemainingAsync();
        }

        public async Task<Document> GetMovie(int userId, string movieName)
        {
            return await table.GetItemAsync(userId, movieName);
        }

        public async Task<IEnumerable<Document>> GetMoviesRanking(string movieName)
        {
            var filter = new QueryFilter(Constants.MovieName_field, QueryOperator.Equal, movieName);
            var config = new QueryOperationConfig()
            {
                IndexName = Models.Constants.TableIndex,
                Filter = filter
            };
            return await table.Query(config).GetRemainingAsync();
        }

        public async Task<IEnumerable<Document>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var filter = new QueryFilter(Constants.UserId_field, QueryOperator.Equal, userId);
            filter.AddCondition(Constants.MovieName_field, QueryOperator.BeginsWith, movieName);

            return await table.Query(filter).GetRemainingAsync();
        }

        public async Task UpdateMovie(Document documentModel)
        {
            await table.UpdateItemAsync(documentModel);
        }
    }
}
