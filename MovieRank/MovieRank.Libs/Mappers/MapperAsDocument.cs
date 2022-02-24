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
                [Constants.ToEnumString(Constants.fieldNames.UserId)] = userId,
                [Constants.ToEnumString(Constants.fieldNames.MovieName)] = movieRankRequest.movieName,
                [Constants.ToEnumString(Constants.fieldNames.Description)] = movieRankRequest.Description,
                [Constants.ToEnumString(Constants.fieldNames.RankDateTime)] = DateTime.UtcNow.ToString(),
                [Constants.ToEnumString(Constants.fieldNames.Actors)] = movieRankRequest.Actors,
                [Constants.ToEnumString(Constants.fieldNames.Ranking)] = movieRankRequest.Ranking
            };
        }

        public Document ToDocumentModel(int userId, MovieResponse movieResponse, MovieUpdateRequest movieRankRequest)
        {
            return new Document
            {
                [Constants.ToEnumString(Constants.fieldNames.UserId)] = userId,
                [Constants.ToEnumString(Constants.fieldNames.MovieName)] = movieResponse.MovieName,
                [Constants.ToEnumString(Constants.fieldNames.Description)] = movieResponse.Description,
                [Constants.ToEnumString(Constants.fieldNames.RankDateTime)] = DateTime.UtcNow.ToString(),
                [Constants.ToEnumString(Constants.fieldNames.Actors)] = movieResponse.Actors,
                [Constants.ToEnumString(Constants.fieldNames.Ranking)] = movieRankRequest.Ranking
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
                description = item[Constants.ToEnumString(Constants.fieldNames.Description)];
            }
            catch
            {
                description = string.Empty;
            }

            string RankDateTime;
            try
            {
                RankDateTime = item[Constants.ToEnumString(Constants.fieldNames.RankDateTime)];
            }
            catch
            {
                RankDateTime = string.Empty;
            }

            return new MovieResponse
            {
                MovieName = item[Constants.ToEnumString(Constants.fieldNames.MovieName)],
                Description = description,
                Actors = item[Constants.ToEnumString(Constants.fieldNames.Actors)].AsListOfString(),
                Ranking = Convert.ToInt32(item[Constants.ToEnumString(Constants.fieldNames.Ranking)]),
                TimeRanked = RankDateTime
            };
        }
    }
}
