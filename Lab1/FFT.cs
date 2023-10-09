using System;
using System.Linq;
using System.Numerics;
using MathNet.Numerics.IntegralTransforms;

namespace DSPLabs;

public static class FFT
{
    public static int[] GetAmplitudes(Complex[] complexData)
    {
        int[] amplitudes = new int[complexData.Length / 2];

        for (int i = 0; i < amplitudes.Length; i++)
        {
            amplitudes[i] = (int)complexData[i].Magnitude;
        }
        return amplitudes;
    }

    public static int BitReverse(int n, int bits)
    {
        int reversedN = n;
        int count = bits - 1;

        n >>= 1;
        while (n > 0)
        {
            reversedN = (reversedN << 1) | (n & 1);
            count--;
            n >>= 1;
        }

        return ((reversedN << count) & ((1 << bits) - 1));
    }

    public static Complex[] Perform(int[] data)
    {
        Complex[] buffer = data.Select(x => new Complex(x, 0)).ToArray();
        int bits = (int)Math.Log(buffer.Length, 2);
        for (int j = 1; j < buffer.Length; j++)
        {
            int swapPos = BitReverse(j, bits);
            if (swapPos <= j)
            {
                continue;
            }
            var temp = buffer[j];
            buffer[j] = buffer[swapPos];
            buffer[swapPos] = temp;
        }
        for (int N = 2; N <= buffer.Length; N <<= 1)
        {
            for (int i = 0; i < buffer.Length; i += N)
            {
                for (int k = 0; k < N / 2; k++)
                {

                    int evenIndex = i + k;
                    int oddIndex = i + k + (N / 2);
                    var even = buffer[evenIndex];
                    var odd = buffer[oddIndex];

                    double term = -2 * Math.PI * k / (double)N;
                    Complex exp = new Complex(Math.Cos(term), Math.Sin(term)) * odd;

                    buffer[evenIndex] = even + exp;
                    buffer[oddIndex] = even - exp;
                }
            }
        }
        return buffer;
    }
}
