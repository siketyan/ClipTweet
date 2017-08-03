using ClipTweet.Resources;
using CoreTweet;
using Newtonsoft.Json;

namespace ClipTweet.Objects
{
    /// <summary>
    /// アカウントの管理用クラス
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 初期状態のリングの色
        /// </summary>
        private const string DEFAULT_RING = "Transperent";

        /// <summary>
        /// アクティブ時のリングの色
        /// </summary>
        private const string ACTIVE_RING = "#39F";


        /// <summary>
        /// プロフィール画像の周りに表示されるリングの色。
        /// アクティブかどうかを表す
        /// </summary>
        [JsonIgnore]
        public string RingColor { get; set; } = DEFAULT_RING;

        /// <summary>
        /// Twitterアカウントのトークン
        /// </summary>
        [JsonIgnore]
        public Tokens Token { get; private set; }

        /// <summary>
        /// Twitterアカウントのユーザ情報。
        /// </summary>
        [JsonIgnore]
        public User User { get; private set; }


        /// <summary>
        /// アカウントのユニークなID
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// アカウントの名前。@~で表記されるもの
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// プロフィール画像へのURL
        /// </summary>
        [JsonProperty("image")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// アカウントにアクセスするためのトークン
        /// </summary>
        [JsonProperty("token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// アカウントにアクセスするためのシークレット
        /// </summary>
        [JsonProperty("secret")]
        public string AccessTokenSecret { get; set; }


        /// <summary>
        /// アカウントがアクティブになっているかどうか
        /// </summary>
        [JsonIgnore]
        public bool IsActive
        {
            get
            {
                return this.RingColor == ACTIVE_RING;
            }
        }


        public void Init()
        {
            this.Token = Tokens.Create(
                TwitterKeys.CONSUMER_KEY,
                TwitterKeys.CONSUMER_SECRET,
                this.AccessToken,
                this.AccessTokenSecret
            );

            this.User = this.Token.Account.VerifyCredentials();
            this.Name = this.User.Name;
            this.ImageUrl = this.User.ProfileImageUrlHttps;
        }

        /// <summary>
        /// アカウントをアクティブにする
        /// </summary>
        public void Activate()
        {
            this.RingColor = ACTIVE_RING;
        }

        /// <summary>
        /// アカウントを非アクティブにする
        /// </summary>
        public void Deactivate()
        {
            this.RingColor = DEFAULT_RING;
        }
    }
}