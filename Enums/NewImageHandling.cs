namespace ClipTweet.Enums
{
    /// <summary>
    /// 新しい画像がクリップボードにコピーされたときの動作
    /// </summary>
    public enum NewImageHandling
    {
        AddImage, // ツイートに画像を追加
        ClearImage, // ツイートの画像を全て削除して追加
        NewTweet // ツイート内容を削除して追加
    }
}