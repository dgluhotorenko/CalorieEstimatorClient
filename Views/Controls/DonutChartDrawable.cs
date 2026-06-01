namespace CalorieClient.Views.Controls;

public class DonutChartDrawable : IDrawable
{
    public float Protein { get; set; }
    public float Fat { get; set; }
    public float Carbs { get; set; }
    public string CenterText { get; set; } = "";

    // Sprig macro palette: lavender / coral / teal
    private static readonly Color ProteinColor = Color.FromArgb("#8B7BD8");
    private static readonly Color FatColor = Color.FromArgb("#E68A6B");
    private static readonly Color CarbsColor = Color.FromArgb("#3DAEA4");
    // Faint translucent track — reads well on the green hero card
    private static readonly Color TrackColor = Color.FromRgba(255, 255, 255, 38);

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var size = Math.Min(dirtyRect.Width, dirtyRect.Height);
        var centerX = dirtyRect.Width / 2;
        var centerY = dirtyRect.Height / 2;
        var strokeWidth = size * 0.13f;
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

        // Track ring
        canvas.StrokeColor = TrackColor;
        canvas.StrokeSize = strokeWidth;
        canvas.StrokeLineCap = LineCap.Butt;
        canvas.DrawEllipse(rect);

        if (total > 0)
        {
            const float startAngle = -90f;
            const float gap = 4f;

            var proteinSweep = proteinCal / total * 360f;
            var fatSweep = fatCal / total * 360f;
            var carbsSweep = carbsCal / total * 360f;

            DrawArc(canvas, rect, strokeWidth, startAngle, proteinSweep, gap, ProteinColor);
            DrawArc(canvas, rect, strokeWidth, startAngle + proteinSweep, fatSweep, gap, FatColor);
            DrawArc(canvas, rect, strokeWidth, startAngle + proteinSweep + fatSweep, carbsSweep, gap, CarbsColor);
        }

        // Center: big number + "KCAL" caption
        if (!string.IsNullOrEmpty(CenterText))
        {
            var numberRect = new RectF(rect.X, centerY - size * 0.28f, rect.Width, size * 0.42f);
            canvas.FontColor = Colors.White;
            canvas.Font = new Microsoft.Maui.Graphics.Font("HankenExtraBold");
            canvas.FontSize = size * 0.22f;
            canvas.DrawString(CenterText, numberRect, HorizontalAlignment.Center, VerticalAlignment.Center);

            var labelRect = new RectF(rect.X, centerY + size * 0.06f, rect.Width, size * 0.18f);
            canvas.Font = new Microsoft.Maui.Graphics.Font("HankenBold");
            canvas.FontSize = size * 0.085f;
            canvas.DrawString("KCAL", labelRect, HorizontalAlignment.Center, VerticalAlignment.Center);
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
        canvas.StrokeLineCap = LineCap.Round;
        canvas.DrawArc(rect, startAngle + offset, startAngle + offset + actualSweep, false, false);
    }
}