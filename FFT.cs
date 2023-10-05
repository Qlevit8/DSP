using System.Numerics;
using MathNet.Numerics.IntegralTransforms;

namespace DSPLabs;

public static class FFT
{
    public static Complex[] Perform(int[] data)
    {
        int length = data.Length;
        Complex[] complexData = new Complex[length];

        for (int i = 0; i < length; i++)
        {
            complexData[i] = new Complex(data[i], 0);
        }

        Fourier.Forward(complexData, FourierOptions.NoScaling);

        return complexData;
    }

    public static int[] GetAmplitudes(Complex[] complexData)
    {
        int[] amplitudes = new int[complexData.Length / 2];

        for (int i = 0; i < amplitudes.Length; i++)
        {
            amplitudes[i] = (int)complexData[i].Magnitude;
        }
        return amplitudes;
    }
}
