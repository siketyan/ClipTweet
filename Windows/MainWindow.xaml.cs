using ClipTweet.Objects;
using ClipTweet.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

using Controls = System.Windows.Controls;

namespace ClipTweet.Windows
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Image> Images { get; set; }

        private ClipboardWatcher watcher;
        private Settings settings;

        public MainWindow()
        {
            InitializeComponent();

            this.settings = Settings.Open();
            this.Images = new ObservableCollection<Image>();
            this.DataContext = this;
        }

        private void Init(object sender, RoutedEventArgs e)
        {
            this.watcher = new ClipboardWatcher(new WindowInteropHelper(this).Handle);
            this.watcher.ClipboardChanged += new EventHandler(OnClipboardChanged);
            this.splashScreen.Visibility = Visibility.Collapsed;
            CloseWindow();
        }
        
        private void DeleteImage(object sender, RoutedEventArgs e)
        {
            var image = (BitmapSource)((Controls.Button)sender).Tag;
            this.Images.Remove(
                this.Images.Where(
                    i => i.Equals(image)
                ).FirstOrDefault()
            );
        }

        private void ShowSettings(object sender, RoutedEventArgs e)
        {
            var window = new SettingsWindow(this.settings)
            {
                Owner = this
            };

            window.ShowDialog();
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
            if (Clipboard.ContainsImage())
            {
                if (this.Images.Where(i => i.Equals(Clipboard.GetImage())).Count() != 0)
                {
                    return;
                }

                ShowWindow();
                this.Images.Add(new Image(Clipboard.GetImage()));
            }
        }
    }
}