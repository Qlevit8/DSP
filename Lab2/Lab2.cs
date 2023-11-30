using Accord.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DSPLabs;

public class Lab2 : ITask<double>
{
    private static Color StartColor = Color.FromRgb(0, 0, 255);
    private static Color EndColor = Color.FromRgb(255, 0, 0);
    public double[]? Outputs { get; set; }

    public void Calculate(int[] fragments)
    {
        Outputs = Perform(fragments);
    }

    public void CalculateAdd(int[] fragments)
    {
        var result = Perform(fragments);
        if (Outputs is null || Outputs.Length == 0)
            Outputs = result;
        List<double> lst = Outputs.ToList();
        lst.AddRange(result);
        Outputs = lst.ToArray();
    }

    public void Draw(Canvas canvas, Style textStyle)
    {
        if (Outputs is null) return;
        canvas.DrawMFCC(Outputs, canvas.ActualWidth / AudioContainer.FragmentSize * 2, 1);
        canvas.AddText("MFCC", textStyle);
    }

    public double[]? Perform(int[] fragments)
    {
        MFCC mfcc = new MFCC();
        var a = mfcc.ExecuteMFCC(FFT.GetAmplitudes(FFT.Perform(fragments)), MFCC.MFCC_SIZE, MFCC.FREQ, MFCC.FREQ_MIN, MFCC.FREQ_MAX, MFCC.FRAME_LENGTH);
        return FFT.GetAmplitudes(FFT.Perform(fragments)).Select(x => (int)x).ToArray().Select(x => (double)x).ToArray();
    }
}
