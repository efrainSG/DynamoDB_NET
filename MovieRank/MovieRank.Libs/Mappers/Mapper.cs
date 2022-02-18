using MovieRank.Contracts;
using MovieRank.Libs.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieRank.Libs.Mappers
{
    public class Mapper : IMapper
    {
        public IEnumerable<MovieResponse>MovieContract(IEnumerable<MovieDb> items) {
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
    }
}
