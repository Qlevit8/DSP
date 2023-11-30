using System;
using System.Windows;
using System.Windows.Controls;

namespace DSPLabs;

public interface ITask<T> : ITask where T : struct, IComparable, IConvertible
{
    public T[]? Outputs { get; set; }
    public T[]? Perform(int[] fragment);
}

public interface ITask
{
    public void Calculate(int[] fragment);
    public void CalculateAdd(int[] fragment);
    public void Draw(Canvas canvas, Style textStyle);
}
