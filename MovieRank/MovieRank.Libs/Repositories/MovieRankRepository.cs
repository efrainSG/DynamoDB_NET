using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
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
            var scanRequest = new ScanRequest(Models.Constants.TableName);
            return await dynamoDBClient.ScanAsync(scanRequest);
        }

        public async Task<GetItemResponse> GetMovie(int userId, string movieName)
        {
            var request = new GetItemRequest
            {
                TableName = Constants.TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {Constants.UserId_field, new AttributeValue {N = userId.ToString() } },
                    {Constants.MovieName_field, new AttributeValue{S = movieName} }
                }
            };

            return await dynamoDBClient.GetItemAsync(request);
        }

        public async Task<QueryResponse> GetUsersRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var request = new QueryRequest
            {
                TableName = Constants.TableName,
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
                TableName = Constants.TableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    {Constants.UserId_field, new AttributeValue { N = userId.ToString() } },
                    {Constants.MovieName_field, new AttributeValue{ S = movieRankRequest.movieName } },
                    {Constants.Description_field, new AttributeValue{ S = movieRankRequest.Description } },
                    {Constants.Actors_field, new AttributeValue{ SS = movieRankRequest.Actors } },
                    {Constants.Ranking_field, new AttributeValue{ N = movieRankRequest.Ranking.ToString() } },
                    {Constants.RankDateTime_field, new AttributeValue { S = DateTime.UtcNow.ToString() } }
                }
            };
            await dynamoDBClient.PutItemAsync(request);
        }

        public async Task UpdateMovie(int userId, MovieUpdateRequest updateRequest)
        {
            var request = new UpdateItemRequest
            {
                TableName = Constants.TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {Constants.UserId_field, new AttributeValue { N = userId.ToString() } },
                    {Constants.MovieName_field, new AttributeValue { S = updateRequest.movieName } }
                },
                AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                    { Constants.Ranking_field, new AttributeValueUpdate
                        {
                            Action = AttributeAction.PUT,
                            Value = new AttributeValue { N = updateRequest.Ranking.ToString() }
                        }
                    },
                    {
                        Constants.RankDateTime_field, new AttributeValueUpdate
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
                TableName = Constants.TableName,
                IndexName = Constants.TableIndex,
                KeyConditionExpression = "MovieName = :movieName",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {":movieName", new AttributeValue{S=movieName} }
                }
            };
            return await dynamoDBClient.QueryAsync(request);
        }
    }
}
