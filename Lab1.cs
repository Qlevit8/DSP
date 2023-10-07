using System.Windows;
using System.Windows.Controls;

namespace DSPLabs;

public class Lab1 : ITask
{
    private int[]? _outputs = null;
    public void Calculate(int[] fragment)
    {
        _outputs = FFT.GetAmplitudes(FFT.Perform(fragment));
    }

    public void Draw(Canvas canvas, Style textStyle)
    {
        if(_outputs is null) return;
        canvas.DrawFFT(_outputs, canvas.ActualWidth / AudioContainer.FragmentSize * 2);
        canvas.AddText("FFT", textStyle);
    }
}
