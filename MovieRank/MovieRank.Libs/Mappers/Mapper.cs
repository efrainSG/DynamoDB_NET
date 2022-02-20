using Amazon.DynamoDBv2.DocumentModel;
using MovieRank.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRank.Libs.Mappers
{
    public class Mapper : IMapper
    {
        public Document ToDocumentModel(int userId, MovieRankRequest movieRankRequest)
        {
            return new Document
            {
                ["UserId"] = userId,
                ["MovieName"] = movieRankRequest.movieName,
                ["Description"] = movieRankRequest.Description,
                ["RankDateTime"] = DateTime.UtcNow.ToString(),
                ["Actors"] = movieRankRequest.Actors,
                ["Ranking"] = movieRankRequest.Ranking
            };
        }

        public Document ToDocumentModel(int userId, MovieResponse movieResponse, MovieUpdateRequest movieRankRequest)
        {
            return new Document
            {
                ["UserId"] = userId,
                ["MovieName"] = movieResponse.MovieName,
                ["Description"] = movieResponse.Description,
                ["RankDateTime"] = DateTime.UtcNow.ToString(),
                ["Actors"] = movieResponse.Actors,
                ["Ranking"] = movieRankRequest.Ranking
            };
        }

        public IEnumerable<MovieResponse> ToMovieContract(IEnumerable<Document> items)
        {
            return items.Select(ToMovieContract);
        }

        public MovieResponse ToMovieContract(Document item)
        {
            string description = string.Empty;
            try
            {
                description = item["Description"];
            }
            catch
            {
                description = string.Empty;
            }


            string RankDateTime = string.Empty;
            try
            {
                RankDateTime = item["RankDateTime"];
            }
            catch
            {
                RankDateTime = string.Empty;
            }

            return new MovieResponse
            {
                MovieName = item["MovieName"],
                Description = description,
                Actors = item["Actors"].AsListOfString(),
                Ranking = Convert.ToInt32(item["Ranking"]),
                TimeRanked = RankDateTime
            };
        }
    }
}
