using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace DSPLabs;

public class InterfaceStyle
{
    public static int CornerRadiusValue { get; private set; } = 15;
    public static int HalfCornerRadiusValue { get; private set; } = CornerRadiusValue / 2;
    public static SolidColorBrush ExitButtonColor { get; private set; } = new SolidColorBrush(Color.FromRgb(239, 59, 62));
    public static SolidColorBrush MinimizeButtonColor { get; private set; } = new SolidColorBrush(Color.FromRgb(241, 222, 21));
    public static SolidColorBrush ButtonInactiveColor { get; private set; } = new SolidColorBrush(Color.FromRgb(166, 8, 8));
    public static int FontSize { get; private set; } = 14;
    public static FontFamily FontFamily { get; private set; } = new FontFamily("Bahnschrift Light");
    public static SolidColorBrush FontColor { get; private set; } = new SolidColorBrush(Color.FromRgb(255, 255, 255));
}
