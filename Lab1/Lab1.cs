using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DSPLabs;

public class Lab1 : ITask<int>
{
    public int[]? Outputs { get; set; }
    public void Calculate(int[] fragment)
    {
        Outputs = FFT.GetAmplitudes(FFT.Perform(fragment)).Select(x => (int)x).ToArray();
    }

    public void CalculateAdd(int[] fragment)
    {
        var result = Perform(fragment);
        if (Outputs is null || Outputs.Length == 0)
            Outputs = result;
        List<int> lst = Outputs.ToList();
        lst.AddRange(result);
        Outputs = lst.ToArray();
    }

    public void Draw(Canvas canvas, Style textStyle)
    {
        if(Outputs is null) return;
        canvas.DrawFFT(Outputs, canvas.ActualWidth / AudioContainer.FragmentSize * 2);
        canvas.AddText("FFT", textStyle);
    }

    public int[]? Perform(int[] fragment)
    {
        return FFT.GetAmplitudes(FFT.Perform(fragment)).Select(x => (int)x).ToArray();
    }
}
