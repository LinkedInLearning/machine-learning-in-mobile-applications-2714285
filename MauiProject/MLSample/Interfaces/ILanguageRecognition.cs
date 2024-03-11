using System;
using MLSample.Models;

namespace MLSample.Interfaces
{
    public interface ILanguageRecognition
    {
        TopScoringIntent GetTopScoringIntent(string enteredText);
    }
}