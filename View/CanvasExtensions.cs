using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DSPLabs;

public static class CanvasExtensions
{
    public static void DrawMFCC(this Canvas canvas, double[] fragment, double coeff, double pixelSize)
    {
        canvas.Children.Clear();
        var width = (int)canvas.ActualWidth;
        var height = (int)canvas.ActualHeight;
        Rectangle rect = new Rectangle() { Height = height, Width = width };
        rect.Fill = InterfaceStyle.MainColors[3];

        Image image = new Image();
        image.Stretch = Stretch.None;
        image.Margin = new Thickness(0);

        canvas.Children.Add(image);

        byte[,,] bytes = new byte[height, width, 4];
        int r = 1, g = 1, b = 1;
        int iteratorG = 1;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                bytes[i, j, 0] = (byte)(r++ % 255);
                bytes[i, j, 1] = (byte)(g+= iteratorG % 255);
                bytes[i, j, 2] = (byte)(b+=g % 255);
                bytes[i, j, 3] = 255;
                iteratorG++;
            }
        }
        image.Source = CustomPainter.WindowLoaded(width, height, bytes);

    }

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
            vertL.Stroke = new SolidColorBrush(Colors.CornflowerBlue);
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
            Line vertL = new Line()
            {
                Stroke = new SolidColorBrush(Colors.ForestGreen),
                StrokeThickness = 4
            };
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
            vertL.Stroke = new SolidColorBrush(Colors.IndianRed);
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
