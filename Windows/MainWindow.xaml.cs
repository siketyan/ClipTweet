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
        public ObservableCollection<Account> Accounts { get; set; }

        private ClipboardWatcher watcher;
        private Settings settings;

        public MainWindow()
        {
            InitializeComponent();

            this.settings = App.GetInstance().Settings;
            this.Images = new ObservableCollection<Image>();
            this.Accounts = new ObservableCollection<Account>();
            this.DataContext = this;

            LoadAccounts();
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

        private void LoadAccounts()
        {
            this.Accounts.Clear();

            foreach (var account in this.settings.Accounts)
            {
                this.Accounts.Add(account);
            }

            if (this.Accounts.Where(a => a.IsActive).Count() == 0)
            {
                this.Accounts[0].Activate();
            }
        }

        private void ActivateAccount(object sender, RoutedEventArgs e)
        {
            var id = (long)((Controls.Button)sender).Tag;
            this.Accounts.Where(a => a.IsActive).FirstOrDefault().Deactivate();
            this.Accounts.Where(a => a.Id == id).FirstOrDefault().Activate();

            this.accountsList.Items.Refresh();
        }

        private void ShowSettings(object sender, RoutedEventArgs e)
        {
            var window = new SettingsWindow()
            {
                Owner = this
            };

            window.ShowDialog();
            LoadAccounts();
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