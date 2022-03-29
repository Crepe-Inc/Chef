using System.Windows;
using System.Runtime.InteropServices;
using System;
using System.Windows.Input;

namespace Chef
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void toTaskbar(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void fromTaskbar(object sender, RoutedEventArgs e)
        {
            this.Show();
        }
        private void exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
