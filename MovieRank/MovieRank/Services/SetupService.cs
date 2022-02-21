using MovieRank.Contracts;
using MovieRank.Libs.Mappers;
using MovieRank.Libs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
