using Amazon.DynamoDBv2.Model;
using MovieRank.Contracts;
using MovieRank.Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRank.Libs.Mappers
{
    public class Mapper : IMapper
    {
        public IEnumerable<MovieResponse> ToMovieContract(ScanResponse response)
        {
            return response.Items.Select(ToMovieContract);
        }

        public MovieResponse ToMovieContract(GetItemResponse response)
        {
            return new MovieResponse
            {
                MovieName = response.Item[Constants.ToEnumString(Constants.fieldNames.MovieName)].S,
                Description = response.Item[Constants.ToEnumString(Constants.fieldNames.Description)].S,
                Actors = response.Item[Constants.ToEnumString(Constants.fieldNames.Actors)].SS,
                Ranking = Convert.ToInt32(response.Item[Constants.ToEnumString(Constants.fieldNames.Ranking)].N),
                TimeRanked = response.Item[Constants.ToEnumString(Constants.fieldNames.RankDateTime)].S
            };
        }

        public IEnumerable<MovieResponse> ToMovieContract(QueryResponse response)
        {
            return response.Items.Select(ToMovieContract);
        }

        private MovieResponse ToMovieContract(Dictionary<string, AttributeValue> item)
        {
            return new MovieResponse
            {
                MovieName = item[Constants.ToEnumString(Constants.fieldNames.MovieName)].S,
                Description = item[Constants.ToEnumString(Constants.fieldNames.Description)].S,
                Actors = item[Constants.ToEnumString(Constants.fieldNames.Actors)].SS,
                Ranking = Convert.ToInt32(item[Constants.ToEnumString(Constants.fieldNames.Ranking)].N),
                TimeRanked = item[Constants.ToEnumString(Constants.fieldNames.RankDateTime)].S
            };
        }
    }
}
