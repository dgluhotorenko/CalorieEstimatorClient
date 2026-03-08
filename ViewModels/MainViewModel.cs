using CalorieClient.Models;
using CalorieClient.Services.Abstract;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalorieClient.ViewModels;

public partial class MainViewModel(ICalorieService calorieService) : ObservableObject
{
    [ObservableProperty]
    private string? _imagePath;

    [ObservableProperty]
    private string? _userNotes;

    [ObservableProperty]
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
        try
        {
            var photo = (await MediaPicker.Default.PickPhotosAsync(new MediaPickerOptions { SelectionLimit = 1 })).FirstOrDefault();

            if (photo != null)
            {
                ImagePath = photo.FullPath;
                IsBusy = true;
                ResultText = "Analyzing...";
                AnalysisResult = null;

                // Send to API
                AnalysisResult = await calorieService.AnalyzeImageAsync(photo, UserNotes);

                if (AnalysisResult != null)
                {
                    ResultText = AnalysisResult.IsFood ? $"Ready! This is {AnalysisResult.DishName}" : "This doesn't look like food :(";
                }
            }
        }
        catch (Exception ex)
        {
            ResultText = $"Error: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task TakePhoto()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo != null)
            {
                ImagePath = photo.FullPath;
                // move to separated method Analyze(FileResult file))
                IsBusy = true;
                AnalysisResult = await calorieService.AnalyzeImageAsync(photo, UserNotes);
                IsBusy = false;
            }
        }
    }
}