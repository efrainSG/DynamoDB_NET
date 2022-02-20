using MovieRank.Contracts;
using MovieRank.Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRank.Libs.Mappers
{
    public class MapperAsTable : IMapperAsTable
    {
        public IEnumerable<MovieResponse>ToMovieContract(IEnumerable<MovieDb> items) {
            return items.Select(ToMovieContract);
        }

        public MovieResponse ToMovieContract(MovieDb movie)
        {
            return new MovieResponse
            {
                MovieName = movie.MovieName,
                Actors = movie.Actors,
                Description = movie.Description,
                Ranking = movie.Ranking,
                TimeRanked = movie.RankDateTime
            };
        }

        public MovieDb ToMovieDBModel(int userId, MovieRankRequest movieRankRequest)
        {
            return new MovieDb
            {
                Actors = movieRankRequest.Actors,
                Description = movieRankRequest.Description,
                MovieName = movieRankRequest.movieName,
                UserId = userId,
                Ranking = movieRankRequest.Ranking,
                RankDateTime = DateTime.UtcNow.ToString()
            };
        }

        public MovieDb ToMovieDBModel(int userId, MovieDb movieRankRequest, MovieUpdateRequest movieUpdateRequest)
        {
            return new MovieDb
            {
                UserId = userId,
                MovieName = movieRankRequest.MovieName,
                Description = movieRankRequest.Description,
                Actors = movieRankRequest.Actors,
                Ranking = movieUpdateRequest.Ranking,
                RankDateTime = DateTime.UtcNow.ToString()
            };
        }
    }
}
