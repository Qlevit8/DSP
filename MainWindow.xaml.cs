using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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

    private void Exit(object sender, RoutedEventArgs e) =>
        Application.Current.Shutdown();

    private void Minimize(object sender, RoutedEventArgs e) =>
        WindowState = WindowState.Minimized;

    private void LoadAudioFile(object sender, RoutedEventArgs e) =>
        AudioContainer.Load();

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
}
