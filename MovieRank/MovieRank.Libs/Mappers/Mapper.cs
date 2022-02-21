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
                MovieName = response.Item[Constants.MovieName_field].S,
                Description = response.Item[Constants.Description_field].S,
                Actors = response.Item[Constants.Actors_field].SS,
                Ranking = Convert.ToInt32(response.Item[Constants.Ranking_field].N),
                TimeRanked = response.Item[Constants.RankDateTime_field].S
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
                MovieName = item[Constants.MovieName_field].S,
                Description = item[Constants.Description_field].S,
                Actors = item[Constants.Actors_field].SS,
                Ranking = Convert.ToInt32(item[Constants.Ranking_field].N),
                TimeRanked = item[Constants.RankDateTime_field].S
            };
        }
    }
}
