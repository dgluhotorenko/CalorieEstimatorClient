using CalorieClient.Services;
using CalorieClient.Services.Abstract;
using CalorieClient.ViewModels;
using CalorieClient.Views;
using Microsoft.Extensions.Logging;

namespace CalorieClient;

public static class MauiProgram
{
    // IMPORTANT: Replace this with your actual API URL!
    // For Android emulator: "http://10.0.2.2:PORT_NUMBER"
    // For iOS real device: your Dev Tunnel URL (https://....devtunnels.ms)
    private const string ApiBaseUrl = "http://10.0.2.2:5064";

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddHttpClient<ICalorieService, CalorieService>(client =>
        {
            client.BaseAddress = new Uri(ApiBaseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        builder.Services.AddSingleton<IHistoryService, HistoryService>();

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddSingleton<HistoryViewModel>();
        builder.Services.AddSingleton<HistoryPage>();

        return builder.Build();
    }
}