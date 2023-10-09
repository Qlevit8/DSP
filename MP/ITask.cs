using System.Windows;
using System.Windows.Controls;

namespace DSPLabs;

public interface ITask
{
    public void Calculate(int[] fragment);
    public void Draw(Canvas canvas, Style textStyle);
}
