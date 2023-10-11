using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DSPLabs;

public class Lab2 : ITask
{
    private static Color StartColor = Color.FromRgb(0, 0, 255);
    private static Color EndColor = Color.FromRgb(255, 0, 0);
    private double[]? _coefficients = null;
    public void Calculate(int[] fragments)
    {
        _coefficients = MFCC.Perform(fragments);
    }

    public void Draw(Canvas canvas, Style textStyle)
    {
        //mfcc cepstrograms
        throw new NotImplementedException();
    }
}
