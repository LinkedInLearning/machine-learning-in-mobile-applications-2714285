using System;
using MLSample.Constants;

namespace MLSample.Models
{
    public class TopScoringIntent
    {
        public UserIntent Intent { get; set; }
        public double Score { get; set; }
    }
}
