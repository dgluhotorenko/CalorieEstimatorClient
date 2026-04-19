using System.Text.Json;
using SQLite;

namespace CalorieClient.Models;

public class HistoryEntry
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string DishName { get; set; } = "";
    public int TotalCalories { get; set; }
    public int ConfidenceScore { get; set; }
    public double TotalProtein { get; set; }
    public double TotalFat { get; set; }
    public double TotalCarbs { get; set; }
    public string? ImagePath { get; set; }
    [Indexed]
    public string? ImageHash { get; set; }
    public string IngredientsJson { get; set; } = "[]";
    public DateTime AnalyzedAt { get; set; }

    [Ignore]
    public List<Ingredient> Ingredients
    {
        get => JsonSerializer.Deserialize<List<Ingredient>>(IngredientsJson) ?? [];
        set => IngredientsJson = JsonSerializer.Serialize(value);
    }

    public static HistoryEntry FromAnalysisResult(FoodAnalysisResult result, string? imagePath, string? imageHash = null) => new()
    {
        DishName = result.DishName,
        TotalCalories = result.TotalCalories,
        ConfidenceScore = result.ConfidenceScore,
        TotalProtein = result.TotalProtein,
        TotalFat = result.TotalFat,
        TotalCarbs = result.TotalCarbs,
        ImagePath = imagePath,
        ImageHash = imageHash,
        Ingredients = result.Ingredients,
        AnalyzedAt = DateTime.Now
    };
}
