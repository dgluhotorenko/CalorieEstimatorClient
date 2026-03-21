using CalorieClient.Models;

namespace CalorieClient.Services.Abstract;

public interface ICalorieService
{
    Task<Result<FoodAnalysisResult>> AnalyzeImageAsync(FileResult file, string? notes);
}