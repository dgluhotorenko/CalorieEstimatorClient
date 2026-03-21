using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using CalorieClient.Models;
using CalorieClient.Services.Abstract;

namespace CalorieClient.Services;

public class CalorieService(HttpClient httpClient) : ICalorieService
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<Result<FoodAnalysisResult>> AnalyzeImageAsync(FileResult file, string? notes)
    {
        try
        {
            using var content = new MultipartFormDataContent();

            await using var stream = await file.OpenReadAsync();
            using var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "image", file.FileName);

            if (!string.IsNullOrWhiteSpace(notes))
            {
                content.Add(new StringContent(notes), "notes");
            }

            var response = await httpClient.PostAsync("/api/analyze", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return Result<FoodAnalysisResult>.Failure($"API Error: {response.StatusCode} - {error}");
            }

            var result = await response.Content.ReadFromJsonAsync<FoodAnalysisResult>(JsonOptions);

            return result is not null
                ? Result<FoodAnalysisResult>.Success(result)
                : Result<FoodAnalysisResult>.Failure("Failed to parse server response");
        }
        catch (TaskCanceledException)
        {
            return Result<FoodAnalysisResult>.Failure("Request timed out. Check your connection and try again.");
        }
        catch (HttpRequestException)
        {
            return Result<FoodAnalysisResult>.Failure("Could not reach the server. Make sure the API is running.");
        }
    }
}