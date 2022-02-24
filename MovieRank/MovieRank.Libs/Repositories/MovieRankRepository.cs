using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using MovieRank.Contracts;
using MovieRank.Libs.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Libs.Repositories
{
    public class MovieRankRepository : IMovieRankRepository
    {
        private readonly IAmazonDynamoDB dynamoDBClient;

        public MovieRankRepository(IAmazonDynamoDB dynamoDBClient)
        {
            this.dynamoDBClient = dynamoDBClient;
        }

        public async Task<ScanResponse> GetAllItems()
        {
            var scanRequest = new ScanRequest(Constants.ToEnumString(Constants.tableNames.MovieRanking001));
            return await dynamoDBClient.ScanAsync(scanRequest);
        }

        public async Task<GetItemResponse> GetMovie(int userId, string movieName)
        {
            var request = new GetItemRequest
            {
                TableName = Constants.ToEnumString(Constants.tableNames.MovieRanking001),
                Key = new Dictionary<string, AttributeValue>
                {
                    {Constants.ToEnumString(Constants.fieldNames.UserId), new AttributeValue {N = userId.ToString() } },
                    {Constants.ToEnumString(Constants.fieldNames.MovieName), new AttributeValue{S = movieName} }
                }
            };

            return await dynamoDBClient.GetItemAsync(request);
        }

        public async Task<QueryResponse> GetUsersRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var request = new QueryRequest
            {
                TableName = Constants.ToEnumString(Constants.tableNames.MovieRanking001),
                KeyConditionExpression = "UserId = :userId and begins_with (MovieName, :movieName)",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {":userId", new AttributeValue{ N = userId.ToString() } },
                    {":movieName", new AttributeValue { S = movieName } }
                }
            };

            return await dynamoDBClient.QueryAsync(request);
        }

        public async Task AddMovie(int userId, MovieRankRequest movieRankRequest)
        {
            var request = new PutItemRequest
            {
                TableName = Constants.ToEnumString(Constants.tableNames.MovieRanking001),
                Item = new Dictionary<string, AttributeValue>
                {
                    {Constants.ToEnumString(Constants.fieldNames.UserId), new AttributeValue { N = userId.ToString() } },
                    {Constants.ToEnumString(Constants.fieldNames.MovieName), new AttributeValue{ S = movieRankRequest.movieName } },
                    {Constants.ToEnumString(Constants.fieldNames.Description), new AttributeValue{ S = movieRankRequest.Description } },
                    {Constants.ToEnumString(Constants.fieldNames.Actors), new AttributeValue{ SS = movieRankRequest.Actors } },
                    {Constants.ToEnumString(Constants.fieldNames.Ranking), new AttributeValue{ N = movieRankRequest.Ranking.ToString() } },
                    {Constants.ToEnumString(Constants.fieldNames.RankDateTime), new AttributeValue { S = DateTime.UtcNow.ToString() } }
                }
            };
            await dynamoDBClient.PutItemAsync(request);
        }

        public async Task UpdateMovie(int userId, MovieUpdateRequest updateRequest)
        {
            var request = new UpdateItemRequest
            {
                TableName = Constants.ToEnumString(Constants.tableNames.MovieRanking001),
                Key = new Dictionary<string, AttributeValue>
                {
                    {Constants.ToEnumString(Constants.fieldNames.UserId), new AttributeValue { N = userId.ToString() } },
                    {Constants.ToEnumString(Constants.fieldNames.MovieName), new AttributeValue { S = updateRequest.movieName } }
                },
                AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                    { Constants.ToEnumString(Constants.fieldNames.Ranking), new AttributeValueUpdate
                        {
                            Action = AttributeAction.PUT,
                            Value = new AttributeValue { N = updateRequest.Ranking.ToString() }
                        }
                    },
                    {
                        Constants.ToEnumString(Constants.fieldNames.RankDateTime), new AttributeValueUpdate
                        {
                            Action = AttributeAction.PUT,
                            Value = new AttributeValue { S = DateTime.UtcNow.ToString() }
                        }
                    }
                }
            };
            await dynamoDBClient.UpdateItemAsync(request);
        }

        public async Task<QueryResponse> GetMoviesRanking(string movieName)
        {
            var request = new QueryRequest
            {
                TableName = Constants.ToEnumString(Constants.tableNames.MovieRanking001),
                IndexName = Constants.ToEnumString(Constants.tableIndexes.MovieName_index),
                KeyConditionExpression = "MovieName = :movieName",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {":movieName", new AttributeValue{S=movieName} }
                }
            };
            return await dynamoDBClient.QueryAsync(request);
        }

        public async Task CreateDynamoDbTable(string dynamoDbtableName)
        {
            var request = new CreateTableRequest
            {
                TableName = dynamoDbtableName,
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = "Id",
                        AttributeType = "N"
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "Id",
                        KeyType = "HASH"
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 1,
                    WriteCapacityUnits = 1
                }
            };
            await dynamoDBClient.CreateTableAsync(request);
        }

        public async Task DeleteDynamoDbTable(string dynamoDbTableName)
        {
            var request = new DeleteTableRequest
            {
                TableName = dynamoDbTableName
            };

            await dynamoDBClient.DeleteTableAsync(request);
        }
    }
}
