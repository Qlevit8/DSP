using Microsoft.Win32;
using System;
using System.IO;

namespace DSPLabs;

public class AudioContainer
{
    private string? _audioPath { get; set; }
    public byte[]? Audio { get; private set; }
    public bool IsEnabled
    {
        get => _audioPath != null && Audio != null;
    }
    public AudioContainer() 
    {
    }

    public void Load()
    {
        OpenFileDialog file = new OpenFileDialog();
        file.DefaultExt = ".mp3";
        file.Filter = "Audio files|*.mp3;*.wav";
        file.ShowDialog();
        var path = file.FileName;
        _audioPath = path;
        Audio = AudioToArray(path);
    }

    public string? GetPath() => IsEnabled ? _audioPath : null;

    static byte[]? AudioToArray(string path)
    {
        byte[]? audioData = null;
        try
        {
            audioData = File.ReadAllBytes(path);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            throw;
        }

        return audioData;
    }

    static void ArrayToAudio(byte[] bytes)
    {
        throw new NotImplementedException();
    }
}
