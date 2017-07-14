using System.Windows;

namespace ClipTweet.Windows
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnTextFocused(object sender, RoutedEventArgs e)
        {
            this.placeHolder.Visibility = Visibility.Hidden;
        }

        private void OnTextUnFocused(object sender, RoutedEventArgs e)
        {
            if (this.text.Text == "")
                this.placeHolder.Visibility = Visibility.Visible;
        }
    }
}