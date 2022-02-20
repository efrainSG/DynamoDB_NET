using Amazon.DynamoDBv2.DocumentModel;
using MovieRank.Contracts;
using MovieRank.Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRank.Libs.Mappers
{
    public class MapperAsDocument : IMapperAsDocument
    {
        public Document ToDocumentModel(int userId, MovieRankRequest movieRankRequest)
        {
            return new Document
            {
                [Constants.UserId_field] = userId,
                [Constants.MovieName_field] = movieRankRequest.movieName,
                [Constants.Description_field] = movieRankRequest.Description,
                [Constants.RankDateTime_field] = DateTime.UtcNow.ToString(),
                [Constants.Actors_field] = movieRankRequest.Actors,
                [Constants.Ranking_field] = movieRankRequest.Ranking
            };
        }

        public Document ToDocumentModel(int userId, MovieResponse movieResponse, MovieUpdateRequest movieRankRequest)
        {
            return new Document
            {
                [Constants.UserId_field] = userId,
                [Constants.MovieName_field] = movieResponse.MovieName,
                [Constants.Description_field] = movieResponse.Description,
                [Constants.RankDateTime_field] = DateTime.UtcNow.ToString(),
                [Constants.Actors_field] = movieResponse.Actors,
                [Constants.Ranking_field] = movieRankRequest.Ranking
            };
        }

        public IEnumerable<MovieResponse> ToMovieContract(IEnumerable<Document> items)
        {
            return items.Select(ToMovieContract);
        }

        public MovieResponse ToMovieContract(Document item)
        {
            string description;
            try
            {
                description = item[Constants.Description_field];
            }
            catch
            {
                description = string.Empty;
            }

            string RankDateTime;
            try
            {
                RankDateTime = item[Constants.RankDateTime_field];
            }
            catch
            {
                RankDateTime = string.Empty;
            }

            return new MovieResponse
            {
                MovieName = item[Constants.MovieName_field],
                Description = description,
                Actors = item[Constants.Actors_field].AsListOfString(),
                Ranking = Convert.ToInt32(item[Constants.Ranking_field]),
                TimeRanked = RankDateTime
            };
        }
    }
}
