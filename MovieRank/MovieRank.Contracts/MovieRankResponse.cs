﻿using System.Collections.Generic;

namespace MovieRank.Contracts
{
    public class MovieRankResponse
    {
        public string MovieName { get; set; }
        public double OverallRanking { get; set; }
    }
}