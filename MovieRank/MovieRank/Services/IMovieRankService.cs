﻿using MovieRank.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Services
{
    public interface IMovieRankService
    {
        Task<IEnumerable<MovieResponse>> GetAllItemsFromDatabase();
        Task<MovieResponse> GetMovie(int userId, string movieName);
        Task<IEnumerable<MovieResponse>> getUsersRankedMoviesByMovieTitle(int userId, string movieName);
    }
}
