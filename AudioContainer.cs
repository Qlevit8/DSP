using MathNet.Numerics.IntegralTransforms;
using Microsoft.Win32;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace DSPLabs;

public class AudioContainer
{
    public const int FragmentSize = 512;
    private string? _audioPath { get; set; }
    private int[]? _audioLeftData { get; set; }
    private int[]? _audioRightData { get; set; }
    public int[][]? Fragments { get; set; }

    public bool IsEnabled
    {
        get => _audioPath is not null && _audioLeftData is not null && _audioRightData is not null;
    }

    public (int, int) this[int index] => !IsEnabled ? (0, 0) : (_audioLeftData[index], _audioRightData[index]);
    public AudioContainer()
    {
    }

    public int[] CutByPieces(int count)
    {
        if (!IsEnabled) return new int[] { };
        var result = new int[count];
        int elements = _audioLeftData.Length / count;
        for (int i = 0; i < count; i++)
        {
            var cur = i * elements;
            result[i] = ((int)_audioLeftData[cur..(cur + elements)].Average(x => x) +
                (int)_audioLeftData[cur..(cur + elements)].Average(x => x)) / 2;
        }
        return result;
    }

    public void CutByFragments()
    {
        if (!IsEnabled) return;
        int counter = 0;
        int[][] data = new int[_audioLeftData.Length / FragmentSize][];
        while ((counter * FragmentSize) + FragmentSize < _audioLeftData.Length)
        {
            data[counter] = _audioLeftData[(counter * FragmentSize)..((counter * FragmentSize) + FragmentSize)];
            counter++;
        }
        Fragments = data;
    }

    public string? Load()
    {
        OpenFileDialog file = new OpenFileDialog();
        file.DefaultExt = ".wav";
        file.Filter = "Audio files|*.wav";
        file.ShowDialog();
        var path = file.FileName;
        if (path == "") return null;
        _audioPath = path;
        (_audioLeftData, _audioRightData) = AudioToWaves(path);
        CutByFragments();
        return _audioPath;
    }

    public void Save(string fileName)
    {
        if (!IsEnabled)
            return;
        WavesToAudio(_audioLeftData, _audioRightData, fileName);
    }

    public string? GetPath() => IsEnabled ? _audioPath : null;
    private static (int[]?, int[]?) AudioToWaves(string path)
    {
        var left = new List<int>();
        var right = new List<int>();
        using (WaveFileReader waveFileReader = new WaveFileReader(path))
        {
            if (waveFileReader.WaveFormat.BitsPerSample != 16 || waveFileReader.WaveFormat.Channels != 2)
            {
                Console.WriteLine("Unsupported audio format. Expected 16-bit stereo.");
                return (null, null);
            }

            byte[] buffer = new byte[4];

            while (waveFileReader.Read(buffer, 0, buffer.Length) > 0)
            {
                short leftSample = BitConverter.ToInt16(buffer, 0);
                short rightSample = BitConverter.ToInt16(buffer, 2);
                left.Add(leftSample);
                right.Add(rightSample);
            }
        }
        return (left.ToArray(), right.ToArray());
    }

    private static void WavesToAudio(int[] leftData, int[] rightData, string fileName)
    {
        string filePath = Path.Combine(GetDirectory(), "outputs", fileName + ".wav");
        int sampleRate = 44100;
        int bitsPerSample = 16;
        int numChannels = 2;
        WaveFormat waveFormat = new WaveFormat(sampleRate, bitsPerSample, numChannels);
        using (WaveFileWriter waveFileWriter = new WaveFileWriter(filePath, waveFormat))
        {
            byte[] left = new byte[leftData.Length * sizeof(int)];
            byte[] right = new byte[rightData.Length * sizeof(int)];
            Buffer.BlockCopy(leftData, 0, left, 0, left.Length);
            Buffer.BlockCopy(rightData, 0, right, 0, right.Length);

            for (int i = 0; i < left.Length; i++)
            {
                waveFileWriter.Write(left, 0, left.Length);
                waveFileWriter.Write(right, 0, right.Length);
            }
        }
    }
    private static string GetDirectory()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        int levelsUp = 4;
        string desiredDirectory = baseDirectory;
        for (int i = 0; i < levelsUp; i++)
        {
            if (Directory.GetParent(desiredDirectory) is DirectoryInfo directory)
                desiredDirectory = directory.FullName;
        }
        return desiredDirectory;
    }
}
