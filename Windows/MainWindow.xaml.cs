using ClipTweet.Utilities;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace ClipTweet.Windows
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        ClipboardWatcher watcher;

        public MainWindow()
        {
            InitializeComponent();
            ShowWindow();
        }

        private void Init(object sender, RoutedEventArgs e)
        {
            this.watcher = new ClipboardWatcher(new WindowInteropHelper(this).Handle);
            this.watcher.ClipboardChanged += new EventHandler(OnClipboardChanged);
            this.splashScreen.Visibility = Visibility.Collapsed;
            CloseWindow();
        }

        private void ShowWindow()
        {
            this.IsHitTestVisible = true;
            this.Visibility = Visibility.Visible;
        }

        private void CloseWindow(object sender = null, RoutedEventArgs e = null)
        {
            this.IsHitTestVisible = false;
            this.Visibility = Visibility.Hidden;
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

        private void OnClipboardChanged(object sender, EventArgs e)
        {
            MessageBox.Show("!");
        }
    }
}