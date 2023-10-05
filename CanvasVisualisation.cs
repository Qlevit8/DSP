using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DSPLabs;

public static class CanvasVisualisation
{
    public static void DrawLines(this Canvas canvas, int[] fragment, double coeff)
    {
        canvas.Children.Clear();
        var width = (int)canvas.ActualWidth;
        var height = (int)canvas.ActualHeight;
        Rectangle rect = new Rectangle() { Height = height, Width = width };
        rect.Fill = InterfaceStyle.MainColors[3];
        canvas.Children.Add(rect);
        double range = (double)height / Math.Max(fragment.Max(), -fragment.Min()) / 2;
        for (int i = 0; i < fragment.Length; i++)
        {
            Line vertL = new Line();
            vertL.Stroke = new SolidColorBrush(Colors.White);
            vertL.X1 = i * coeff;
            vertL.X2 = i * coeff;
            vertL.Y1 = height / 2;
            vertL.Y2 = height / 2 - fragment[i] * range;
            canvas.Children.Add(vertL);
        }
    }    
    public static void DrawFFT(this Canvas canvas, int[] fragment, double coeff)
    {
        canvas.Children.Clear();
        var width = (int)canvas.ActualWidth;
        var height = (int)canvas.ActualHeight;
        Rectangle rect = new Rectangle() { Height = height, Width = width };
        rect.Fill = InterfaceStyle.MainColors[3];
        canvas.Children.Add(rect);
        double range = (double)height / Math.Max(fragment.Max(), -fragment.Min());
        for (int i = 0; i < fragment.Length; i++)
        {
            Line vertL = new Line();
            vertL.Stroke = new SolidColorBrush(Colors.White);
            vertL.X1 = i * coeff;
            vertL.X2 = i * coeff;
            vertL.Y1 = height;
            vertL.Y2 = height - fragment[i] * range;
            canvas.Children.Add(vertL);
        }
    }

    public static void DrawWaves(this Canvas canvas, int[] fragment, double coeff)
    {
        canvas.Children.Clear();
        var width = (int)canvas.ActualWidth;
        var height = (int)canvas.ActualHeight;
        Line coord = new Line() { X1 = 0, X2 = width, Y1 = height / 2, Y2 = height / 2 };
        coord.Stroke = new SolidColorBrush(Colors.LightGray);
        canvas.Children.Add(coord);
        double range = (double)height / Math.Max(fragment.Max(), -fragment.Min()) / 2;
        (double x, double y) previous = (0, height / 2 - fragment[0] * range);
        for (int i = 1; i < fragment.Length; i++)
        {
            (double x, double y) cur = (i * coeff, height / 2 - fragment[i] * range);
            Line vertL = new Line();
            vertL.Stroke = new SolidColorBrush(Colors.White);
            vertL.X1 = previous.x;
            vertL.X2 = cur.x;
            vertL.Y1 = previous.y;
            vertL.Y2 = cur.y;
            previous = cur;
            canvas.Children.Add(vertL);
        }
    }

    public static void AddText(this Canvas canvas, string text, Style style)
    {
        Label label = new Label()
        {
            Style = style,
            Content = text,
            Width = canvas.ActualWidth,
            HorizontalContentAlignment = HorizontalAlignment.Right,
        };
        canvas.Children.Add(label);
    }
}
