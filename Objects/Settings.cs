using ClipTweet.Enums;
using Newtonsoft.Json;
using System.IO;

namespace ClipTweet.Objects
{
    /// <summary>
    /// 設定ファイル
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// 設定ファイル名
        /// </summary>
        [JsonIgnore]
        private const string FILE_NAME = "settings.json";


        /// <summary>
        /// 新しい画像がクリップボードにコピーされたときの動作
        /// </summary>
        [JsonProperty("on_new_image")]
        public NewImageHandling OnNewImage { get; set; }


        /// <summary>
        /// 設定ファイルを開く
        /// </summary>
        /// <returns>このクラス</returns>
        public static Settings Open()
        {
            var json = File.ReadAllText(FILE_NAME); // JSONファイルを読み込み
            return JsonConvert.DeserializeObject<Settings>(json); // デシリアライズ
        }

        /// <summary>
        /// 設定ファイルを保存
        /// </summary>
        public void Save()
        {
            var json = JsonConvert.SerializeObject(this); // シリアライズ
            File.WriteAllText(FILE_NAME, json); // JSONファイルに書き込み
        }
    }
}