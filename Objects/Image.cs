using System.Windows.Media.Imaging;

namespace ClipTweet.Objects
{
    public class Image
    {
        public BitmapSource Source { get; set; }

        public Image(BitmapSource src)
        {
            this.Source = src;
        }
    }
}