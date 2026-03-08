using CalorieClient.Models;

namespace CalorieClient.Services.Abstract;

public interface ICalorieService
{
    Task<FoodAnalysisResult?> AnalyzeImageAsync(FileResult file, string? notes);
}