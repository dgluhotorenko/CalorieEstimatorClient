namespace CalorieClient.Views.Controls;

public class DonutChartView : GraphicsView
{
    private readonly DonutChartDrawable _drawable = new();

    public DonutChartView()
    {
        Drawable = _drawable;
    }

    public static readonly BindableProperty ProteinProperty = BindableProperty.Create(
        nameof(Protein), typeof(double), typeof(DonutChartView), 0.0, propertyChanged: OnDataChanged);

    public static readonly BindableProperty FatProperty = BindableProperty.Create(
        nameof(Fat), typeof(double), typeof(DonutChartView), 0.0, propertyChanged: OnDataChanged);

    public static readonly BindableProperty CarbsProperty = BindableProperty.Create(
        nameof(Carbs), typeof(double), typeof(DonutChartView), 0.0, propertyChanged: OnDataChanged);

    public static readonly BindableProperty CenterTextProperty = BindableProperty.Create(
        nameof(CenterText), typeof(string), typeof(DonutChartView), "", propertyChanged: OnDataChanged);

    public double Protein { get => (double)GetValue(ProteinProperty); set => SetValue(ProteinProperty, value); }
    public double Fat { get => (double)GetValue(FatProperty); set => SetValue(FatProperty, value); }
    public double Carbs { get => (double)GetValue(CarbsProperty); set => SetValue(CarbsProperty, value); }
    public string CenterText { get => (string)GetValue(CenterTextProperty); set => SetValue(CenterTextProperty, value); }

    private static void OnDataChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = (DonutChartView)bindable;
        view._drawable.Protein = (float)view.Protein;
        view._drawable.Fat = (float)view.Fat;
        view._drawable.Carbs = (float)view.Carbs;
        view._drawable.CenterText = view.CenterText;
        view.Invalidate();
    }
}
