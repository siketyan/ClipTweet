using ClipTweet.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
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
        /// アカウント
        /// </summary>
        [JsonProperty("accounts")]
        public List<Account> Accounts { get; set; } = new List<Account>();

        /// <summary>
        /// 新しい画像がクリップボードにコピーされたときの動作
        /// </summary>
        [JsonProperty("on_new_image")]
        public NewImageHandling OnNewImage { get; set; } = NewImageHandling.AddImage;

        /// <summary>
        /// ウィンドウの初期位置
        /// </summary>
        [JsonProperty("window_location")]
        public WindowLocation Location { get; set; } = WindowLocation.BottomRight;

        /// <summary>
        /// ウィンドウを表示するスクリーンのインデックス
        /// </summary>
        public int WindowScreen { get; set; } = 0;

        /// <summary>
        /// ウィンドウの初期マージン (縦)
        /// </summary>
        [JsonProperty("vertical_margin")]
        public int VerticalMargin { get; set; } = 50;

        /// <summary>
        /// ウィンドウの初期マージン (横)
        /// </summary>
        [JsonProperty("horizontal_margin")]
        public int HorizontalMargin { get; set; } = 50;


        /// <summary>
        /// 設定ファイルを開く
        /// </summary>
        /// <returns>このクラス</returns>
        public static Settings Open()
        {
            try
            {
                var json = File.ReadAllText(FILE_NAME); // JSONファイルを読み込み
                return JsonConvert.DeserializeObject<Settings>(json); // デシリアライズ
            }
            catch (FileNotFoundException) // JSONファイルが存在しない場合
            {
                return new Settings(); // インスタンスを新規作成
            }
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