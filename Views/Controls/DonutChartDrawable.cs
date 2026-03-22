namespace CalorieClient.Views.Controls;

public class DonutChartDrawable : IDrawable
{
    public float Protein { get; set; }
    public float Fat { get; set; }
    public float Carbs { get; set; }
    public string CenterText { get; set; } = "";

    private static readonly Color ProteinColor = Color.FromArgb("#FF9500");
    private static readonly Color FatColor = Color.FromArgb("#FF3B30");
    private static readonly Color CarbsColor = Color.FromArgb("#34C759");
    private static readonly Color EmptyColor = Color.FromArgb("#E0E0E0");

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var size = Math.Min(dirtyRect.Width, dirtyRect.Height);
        var centerX = dirtyRect.Width / 2;
        var centerY = dirtyRect.Height / 2;
        var strokeWidth = size * 0.15f;
        // Enough inset so the stroke is never clipped
        var radius = (size - strokeWidth) / 2 - 2;

        canvas.Antialias = true;

        // Caloric contribution: protein 4 cal/g, fat 9 cal/g, carbs 4 cal/g
        var proteinCal = Protein * 4;
        var fatCal = Fat * 9;
        var carbsCal = Carbs * 4;
        var total = proteinCal + fatCal + carbsCal;

        var rect = new RectF(
            centerX - radius,
            centerY - radius,
            radius * 2,
            radius * 2);

        if (total <= 0)
        {
            canvas.StrokeColor = EmptyColor;
            canvas.StrokeSize = strokeWidth;
            canvas.StrokeLineCap = LineCap.Butt;
            canvas.DrawEllipse(rect);
        }
        else
        {
            var startAngle = -90f;
            var gap = 3f;

            var proteinSweep = proteinCal / total * 360f;
            var fatSweep = fatCal / total * 360f;
            var carbsSweep = carbsCal / total * 360f;

            DrawArc(canvas, rect, strokeWidth, startAngle, proteinSweep, gap, ProteinColor);
            DrawArc(canvas, rect, strokeWidth, startAngle + proteinSweep, fatSweep, gap, FatColor);
            DrawArc(canvas, rect, strokeWidth, startAngle + proteinSweep + fatSweep, carbsSweep, gap, CarbsColor);
        }

        // Center text
        if (!string.IsNullOrEmpty(CenterText))
        {
            canvas.FontSize = size * 0.18f;
            canvas.FontColor = Colors.White;
            canvas.Font = Microsoft.Maui.Graphics.Font.Default;
            canvas.DrawString(CenterText, rect, HorizontalAlignment.Center, VerticalAlignment.Center);
        }
    }

    private static void DrawArc(ICanvas canvas, RectF rect, float strokeWidth,
        float startAngle, float sweepAngle, float gap, Color color)
    {
        if (sweepAngle < 0.5f) return;

        var actualSweep = Math.Max(sweepAngle - gap, 0.5f);
        var offset = gap / 2;

        canvas.StrokeColor = color;
        canvas.StrokeSize = strokeWidth;
        canvas.StrokeLineCap = LineCap.Butt;
        canvas.DrawArc(rect, startAngle + offset, startAngle + offset + actualSweep, false, false);
    }
}
