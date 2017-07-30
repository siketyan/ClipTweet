using ClipTweet.Resources;
using CoreTweet;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;

namespace ClipTweet.Windows
{
    /// <summary>
    /// TwitterAuthWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TwitterAuthWindow : Window
    {
        public Tokens Result { get; private set; }

        private OAuth.OAuthSession session;

        public TwitterAuthWindow()
        {
            InitializeComponent();

            this.session = OAuth.Authorize(
                TwitterKeys.CONSUMER_KEY,
                TwitterKeys.CONSUMER_SECRET,
                "cliptweet://callback"
            );

            this.browser.Source = this.session.AuthorizeUri;
        }

        private async void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            var uri = e.Uri;
            if (!uri.ToString().StartsWith("cliptweet://callback")) return;
            e.Cancel = true;

            var verifier = uri.Query
                              .Split('&')
                              .Where(q => q.Contains("oauth_verifier")).First()
                              .Split('=')[1];

            this.Result = await this.session.GetTokensAsync(verifier);
            Close();
        }
    }
}