using MovieRank.Contracts;
using MovieRank.Libs.Models;
using System.Collections.Generic;

namespace MovieRank.Libs.Mappers
{
    public interface IMapperAsTable
    {
        IEnumerable<MovieResponse> ToMovieContract(IEnumerable<MovieDb> items);
        MovieResponse ToMovieContract(MovieDb movie);
        MovieDb ToMovieDBModel(int userId, MovieRankRequest movieRankRequest);
        MovieDb ToMovieDBModel(int userId, MovieDb movieRankRequest, MovieUpdateRequest movieUpdateRequest);
    }
}
