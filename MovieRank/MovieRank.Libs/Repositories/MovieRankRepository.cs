﻿using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using MovieRank.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Libs.Repositories
{
    public class MovieRankRepository : IMovieRankRepository
    {
        private readonly DynamoDBContext context;

        public MovieRankRepository(IAmazonDynamoDB dynamoDbClinet)
        {
            context = new DynamoDBContext(dynamoDbClinet);
        }

        public async Task<IEnumerable<MovieDb>> GetAllItems()
        {
            return await context.ScanAsync<MovieDb>(new List<ScanCondition>()).GetRemainingAsync(); //expensive on cost
        }

        public async Task<MovieDb> GetMovie(int userId, string movieName)
        {
            return await context.LoadAsync<MovieDb>(userId, movieName);
        }

        public async Task<IEnumerable<MovieDb>> getUsersRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var config = new DynamoDBOperationConfig
            {
                QueryFilter = new List<ScanCondition>
                {
                    new ScanCondition("MovieName", Amazon.DynamoDBv2.DocumentModel.ScanOperator.BeginsWith, movieName)
                }
            };

            return await context.QueryAsync<MovieDb>(userId, config).GetRemainingAsync();
        }
    }
}
