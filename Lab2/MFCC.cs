using NAudio.Wave;
using System;
using System.Linq;

namespace DSPLabs;

public static class MFCC
{

    public static double[] Perform(int[] fragment)
    {
        var mfcc = FFT.GetAmplitudes(FFT.Perform(fragment)); //FFT
        mfcc = mfcc.Select(Convert).ToArray(); // Filter Bank + log
        mfcc = FFT.GetAmplitudes(FFT.Perform(fragment)); // FFT
        return mfcc;
    }
    public static double Convert(double f) => 1127 * Math.Log(1 + f / 700);
}
