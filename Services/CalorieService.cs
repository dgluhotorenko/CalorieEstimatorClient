using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using CalorieClient.Models;
using CalorieClient.Services.Abstract;

namespace CalorieClient.Services;

public class CalorieService : ICalorieService
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri(BaseUrl),
        Timeout = TimeSpan.FromSeconds(30)
    };
    
    // IMPORTANT: Replace this with your actual API URL!
    // For Android emulator, this is usually "http://10.0.2.2:PORT_NUMBER"
    // For iOS real device - your Dev Tunnel URL (https://....devtunnels.ms)
    private const string BaseUrl = "http://10.0.2.2:5064";

    public async Task<FoodAnalysisResult?> AnalyzeImageAsync(FileResult file, string? notes)
    {
        try
        {
            using var content = new MultipartFormDataContent();

            // 1. add image
            await using var stream = await file.OpenReadAsync();
            using var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "image", file.FileName);

            // 2. add notes if provided
            if (!string.IsNullOrWhiteSpace(notes))
            {
                content.Add(new StringContent(notes), "notes");
            }

            // 3. send a request
            var response = await _httpClient.PostAsync("/api/analyze", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {error}");
            }

            return await response.Content.ReadFromJsonAsync<FoodAnalysisResult>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            // In a real application, throw the error or return a Result<T>
            throw; 
        }
    }
}