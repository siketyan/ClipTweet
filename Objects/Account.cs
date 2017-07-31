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