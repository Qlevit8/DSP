using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace DSPLabs;

public class Lab3 : ITask<double>
{
    public double[]? Outputs { get; set; }

    public void Calculate(int[] fragment)
    {
        throw new NotImplementedException();
    }

    public void CalculateAdd(int[] fragment)
    {
        double[] result = fragment.Select(x => (double)x).ToArray();
        if (Outputs is null || Outputs.Length == 0)
            Outputs = result;
        List<double> lst = Outputs.ToList();
        lst.AddRange(result);
        Outputs = lst.ToArray();
    }

    public void Draw(Canvas canvas, Style textStyle)
    {
        int count = (int)canvas.ActualWidth * 10;
        var result = new int[count];
        int elements = Outputs.Length / count;
        for (int i = 0; i < count; i++)
        {
            var cur = i * elements;
            result[i] = ((int)Outputs[cur..(cur + elements)].Average(x => x) +
                (int)Outputs[cur..(cur + elements)].Average(x => x)) / 2;
        }
        canvas.DrawLines(result, 0.1);
    }

    public double[]? Perform(int[] fragment)
    {
        throw new NotImplementedException();
    }
}
