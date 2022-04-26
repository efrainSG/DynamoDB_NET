using Amazon.DynamoDBv2.Model;
using MovieRank.Contracts;
using System.Collections.Generic;

namespace MovieRank.Libs.Mappers
{
    public interface IMapper
    {
        MovieResponse ToMovieContract(GetItemResponse response);
        IEnumerable<MovieResponse> ToMovieContract(ScanResponse response);
        IEnumerable<MovieResponse> ToMovieContract(QueryResponse response);
    }
}
