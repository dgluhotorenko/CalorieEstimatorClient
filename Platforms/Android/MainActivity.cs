using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Views;

namespace CalorieClient;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        ApplyStatusBarTheme();
    }

    public override void OnConfigurationChanged(Configuration newConfig)
    {
        base.OnConfigurationChanged(newConfig);
        ApplyStatusBarTheme();
    }

    // Sprig status bar: tinted to match the app background, icons inverted for contrast.
    private void ApplyStatusBarTheme()
    {
        if (Window is not { } window)
            return;

        var isDark = (Resources?.Configuration?.UiMode & UiMode.NightMask) == UiMode.NightYes;
        var barColor = isDark ? "#0F130E" : "#F4F6F0";

#pragma warning disable CA1422 // SetStatusBarColor: deprecated only on Android 35+, still correct for min SDK 21
        window.SetStatusBarColor(Android.Graphics.Color.ParseColor(barColor));
#pragma warning restore CA1422

        // Light status bar => dark icons. Dark status bar => light icons.
        if (OperatingSystem.IsAndroidVersionAtLeast(30) && window.InsetsController is { } controller)
        {
            var appearance = isDark ? 0 : (int)WindowInsetsControllerAppearance.LightStatusBars;
            controller.SetSystemBarsAppearance(appearance, (int)WindowInsetsControllerAppearance.LightStatusBars);
        }
        else if (OperatingSystem.IsAndroidVersionAtLeast(23) && window.DecorView is { } decor)
        {
#pragma warning disable CA1422 // SystemUiFlags is the correct API below API 30
            decor.SystemUiFlags = isDark ? 0 : SystemUiFlags.LightStatusBar;
#pragma warning restore CA1422
        }
    }
}
