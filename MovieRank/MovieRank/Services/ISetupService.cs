using System.Threading.Tasks;

namespace MovieRank.Services
{
    public interface ISetupService
    {
        Task CreateDynamoDbTable(string dynamoDbtableName);
        Task DeleteDynamoDbTable(string dynamoDbTableName);
    }
}
