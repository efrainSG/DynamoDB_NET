using MovieRank.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Services
{
    public interface ISetupService
    {
        Task CreateDynamoDbTable(string dynamoDbtableName);
    }
}
