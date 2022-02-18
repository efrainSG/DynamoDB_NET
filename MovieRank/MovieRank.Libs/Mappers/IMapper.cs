using MovieRank.Contracts;
using MovieRank.Libs.Models;
using System.Collections.Generic;

namespace MovieRank.Libs.Mappers
{
    public interface IMapper
    {
        IEnumerable<MovieResponse> MovieContract(IEnumerable<MovieDb> items);
        MovieResponse ToMovieContract(MovieDb movie);
    }
}
