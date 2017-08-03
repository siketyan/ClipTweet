using ClipTweet.Objects;
using ClipTweet.Windows;
using System.Windows;

namespace ClipTweet
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        public Settings Settings { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.Settings = Settings.Open();
            foreach (var account in this.Settings.Accounts)
            {
                account.Init();
            }

            this.Settings.Save();

            new MainWindow().Show();
        }

        public static App GetInstance()
        {
            return ((App)Current);
        }
    }
}