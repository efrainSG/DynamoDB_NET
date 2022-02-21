using MovieRank.Libs.Repositories;
using System.Threading.Tasks;

namespace MovieRank.Services
{
    public class SetupService : ISetupService
    {
        private readonly IMovieRankRepository movieRankRepository;

        public SetupService(IMovieRankRepository movieRankRepository)
        {
            this.movieRankRepository = movieRankRepository;
        }

        public async Task CreateDynamoDbTable(string dynamoDbtableName)
        {
            await movieRankRepository.CreateDynamoDbTable(dynamoDbtableName);
        }

        public async Task DeleteDynamoDbTable(string dynamoDbTableName)
        {
            await movieRankRepository.DeleteDynamoDbTable(dynamoDbTableName);
        }
    }
}
