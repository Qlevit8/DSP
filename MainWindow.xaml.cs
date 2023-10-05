using System.Windows;
using System.Windows.Input;
using Path = System.IO.Path;

namespace DSPLabs;

public partial class MainWindow : Window
{
    public InterfaceStyle Interface { get; private set; }
    public AudioContainer AudioContainer { get; private set; }
    private bool isDragging = false;
    private Point startPoint;
    public MainWindow()
    {
        AudioContainer = new AudioContainer();
        Interface = new InterfaceStyle();
        InitializeComponent();
        DataContext = this;
    }

    private void PlayAudioFile(object sender, RoutedEventArgs e)
    {
        if (AudioContainer.GetPath() is not string path)
            return;
        else
        {
            var player = AudioPlayer;
            player.Source = new System.Uri(path);
            player.IsMuted = false;
            AudioPlayer.Play();
        }
    }

    private void PauseAudio(object sender, RoutedEventArgs e)
    {
        if (AudioPlayer.IsMuted)
            AudioPlayer.Play();
        else
            AudioPlayer.Pause();
        AudioPlayer.IsMuted = !AudioPlayer.IsMuted;
    }

    private void LoadAudioFile(object sender, RoutedEventArgs e)
    {
        var path = AudioContainer.Load();
        if (!AudioContainer.IsEnabled || path is null)
            return;
        FileName.Text = Path.GetFileNameWithoutExtension(path);
        var pieces = AudioContainer.CutByPieces((int)AudioCanvas.ActualWidth * 10);
        AudioCanvas.DrawLines(pieces, 0.1);
    }
    private void SaveAudioFile(object sender, RoutedEventArgs e)
    {
        AudioContainer.Save(FileName.Text);
    }

    private void WindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            startPoint = e.GetPosition(this);
            isDragging = true;
        }
    }

    private void WindowMouseMove(object sender, MouseEventArgs e)
    {
        if (isDragging)
        {
            Point endPoint = e.GetPosition(this);
            double offsetX = endPoint.X - startPoint.X;
            double offsetY = endPoint.Y - startPoint.Y;

            Left += offsetX;
            Top += offsetY;
        }
    }

    private void WindowMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
            isDragging = false;
    }    
    private void ShowAudioFragment(object sender, MouseButtonEventArgs e)
    {
        var x = e.GetPosition(AudioCanvas).X;
        if (!AudioContainer.IsEnabled || x >= AudioCanvas.ActualWidth) return;
        var coef = AudioContainer.Fragments.Length / AudioCanvas.ActualWidth;
        var position = (int)(x * coef);
        FragmentCanvas.DrawWaves(AudioContainer.Fragments[position], AudioCanvas.ActualWidth / AudioContainer.FragmentSize);
        FragmentCanvas.AddText($"Fragment {position} (Size: {AudioContainer.FragmentSize})", FindResource("LabelStyle") as Style);
        TransformationResult.DrawFFT(FFT.GetAmplitudes(FFT.Perform(AudioContainer.Fragments[position])), AudioCanvas.ActualWidth / AudioContainer.FragmentSize * 2);
        TransformationResult.AddText("FFT", FindResource("LabelStyle") as Style);
    }

    private void Exit(object sender, RoutedEventArgs e) =>
    Application.Current.Shutdown();

    private void Minimize(object sender, RoutedEventArgs e) =>
        WindowState = WindowState.Minimized;
}
