using CalorieClient.ViewModels;

namespace CalorieClient.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}