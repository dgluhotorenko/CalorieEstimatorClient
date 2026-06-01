using System.Security.Cryptography;
using CalorieClient.Models;
using CalorieClient.Services.Abstract;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalorieClient.ViewModels;

public partial class MainViewModel(ICalorieService calorieService, IHistoryService historyService) : ObservableObject
{
    private FileResult? _selectedPhoto;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasImage))]
    [NotifyCanExecuteChangedFor(nameof(EstimateCaloriesCommand))]
    private string? _imagePath;

    public bool HasImage => !string.IsNullOrEmpty(ImagePath);

    [ObservableProperty]
    private string? _userNotes;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(EstimateCaloriesCommand))]
    private bool _isBusy;

    [ObservableProperty]
    private string? _resultText;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowResults))]
    private FoodAnalysisResult? _analysisResult;

    public bool ShowResults => AnalysisResult != null;

    [RelayCommand]
    private async Task PickImage()
    {
        var photo = (await MediaPicker.Default.PickPhotosAsync(new MediaPickerOptions { SelectionLimit = 1 })).FirstOrDefault();

        if (photo != null)
            SelectPhoto(photo);
    }

    [RelayCommand]
    private async Task TakePhoto()
    {
        if (!MediaPicker.Default.IsCaptureSupported)
            return;

        var photo = await MediaPicker.Default.CapturePhotoAsync();

        if (photo != null)
            SelectPhoto(photo);
    }

    // Step 1 — stage the photo for review; analysis happens on demand (step 2).
    private void SelectPhoto(FileResult photo)
    {
        _selectedPhoto = photo;
        ImagePath = photo.FullPath;
        AnalysisResult = null;
        ResultText = null;
    }

    // Step 2 — analyse the staged photo.
    [RelayCommand(CanExecute = nameof(CanEstimate))]
    private async Task EstimateCalories()
    {
        if (_selectedPhoto != null)
            await AnalyzeAsync(_selectedPhoto);
    }

    private bool CanEstimate() => HasImage && !IsBusy;

    private async Task AnalyzeAsync(FileResult photo)
    {
        IsBusy = true;
        ResultText = "Analyzing...";
        AnalysisResult = null;

        var imageHash = await ComputeHashAsync(photo.FullPath);

        // Check cache — skip API call if we've seen this image before
        var cached = await historyService.FindByHashAsync(imageHash);
        if (cached != null)
        {
            AnalysisResult = new FoodAnalysisResult
            {
                IsFood = true,
                DishName = cached.DishName,
                TotalCalories = cached.TotalCalories,
                ConfidenceScore = cached.ConfidenceScore,
                Ingredients = cached.Ingredients
            };
            ResultText = $"Ready! This is {cached.DishName} (cached)";
            IsBusy = false;
            return;
        }

        var result = await calorieService.AnalyzeImageAsync(photo, UserNotes);

        if (result.IsSuccess)
        {
            AnalysisResult = result.Value;
            ResultText = AnalysisResult!.IsFood
                ? $"Ready! This is {AnalysisResult.DishName}"
                : "This doesn't look like food :(";

            if (AnalysisResult.IsFood)
            {
                var entry = HistoryEntry.FromAnalysisResult(AnalysisResult, ImagePath, imageHash);
                await historyService.SaveAsync(entry);
            }
        }
        else
        {
            ResultText = result.Error;
        }

        IsBusy = false;
    }

    private static async Task<string> ComputeHashAsync(string filePath)
    {
        await using var stream = File.OpenRead(filePath);
        var hashBytes = await SHA256.HashDataAsync(stream);
        return Convert.ToHexString(hashBytes);
    }
}
