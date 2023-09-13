using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DSPLabs
{
    public partial class MainWindow : Window
    {
        public InterfaceElements Interface { get; private set; }
        private bool isDragging = false;
        private Point startPoint;
        public MainWindow()
        {
            Interface = new InterfaceElements();
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
    }
}
