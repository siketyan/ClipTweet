using ClipTweet.Enums;
using ClipTweet.Objects;
using ClipTweet.Utilities;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Forms = System.Windows.Forms;

namespace ClipTweet.Windows
{
    /// <summary>
    /// SettingsWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public ObservableCollection<Account> Accounts { get; set; }

        private Settings settings;
        
        public SettingsWindow(Settings s)
        {
            InitializeComponent();

            this.settings = s;
            this.Accounts = new ObservableCollection<Account>();

            foreach (var account in this.settings.Accounts)
            {
                this.Accounts.Add(account);
            }

            var screens = Forms.Screen.AllScreens;
            foreach (var screen in screens)
            {
                this.windowScreen.Items.Add(screen.DeviceName);
            }

            var screensCount = screens.Length;
            if (this.settings.WindowScreen >= screensCount)
            {
                this.settings.WindowScreen = screensCount - 1;
            }

            this.newImageHandling.SelectedIndex = (int)this.settings.OnNewImage;
            this.windowLocation.SelectedIndex = (int)this.settings.Location;
            this.windowScreen.SelectedIndex = this.settings.WindowScreen;
            this.verticalMargin.Text = this.settings.VerticalMargin.ToString();
            this.horizontalMargin.Text = this.settings.HorizontalMargin.ToString();
            this.DataContext = this;
        }

        private async void AddAccount(object sender, RoutedEventArgs e)
        {
            var window = new TwitterAuthWindow();
            window.ShowDialog();

            var token = window.Result;
            var user = await token.Account.VerifyCredentialsAsync();
            var account = new Account
            {
                Id = (long)user.Id,
                Name = user.Name,
                ImageUrl = user.ProfileImageUrlHttps,
                AccessToken = token.AccessToken,
                AccessTokenSecret = token.AccessTokenSecret
            };

            if (this.Accounts.Where(a => a.Id == account.Id).Count() != 0)
            {
                MessageBox.Show("The account already exists.");
                return;
            }

            this.Accounts.Add(account);
            this.settings.Accounts.Add(account);
            this.settings.Save();
        }

        private void DeleteAccount(object sender, RoutedEventArgs e)
        {
            var id = (long)((Button)sender).Tag;
            this.Accounts.Remove(
                this.Accounts
                    .Where(a => a.Id == id)
                    .FirstOrDefault()
            );
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.settings.OnNewImage = this.newImageHandling.SelectedIndex.ToEnum<NewImageHandling>();
            this.settings.Location = this.windowLocation.SelectedIndex.ToEnum<WindowLocation>();
            this.settings.WindowScreen = this.windowScreen.SelectedIndex;

            try
            {
                this.settings.VerticalMargin = int.Parse(this.verticalMargin.Text);
                this.settings.HorizontalMargin = int.Parse(this.horizontalMargin.Text);
            }
            catch
            {
                MessageBox.Show("Invalid number was provided by margin textbox.");
                return;
            }

            this.settings.Save();
            Close();
        }

        private void ParseMargin(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender;
            var isNum = int.TryParse(textBox.Text + e.Text, out var margin);
            if (!isNum)
            {
                e.Handled = true;
                return;
            }

            var screen = Forms.Screen.AllScreens[this.windowScreen.SelectedIndex];
            var max = sender == this.verticalMargin
                          ? screen.Bounds.Height / 2
                          : screen.Bounds.Width / 2;
            if (margin > max)
            {
                e.Handled = true;
                textBox.Text = max.ToString();
            }
        }

        private void DisablePaste(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }
    }
}