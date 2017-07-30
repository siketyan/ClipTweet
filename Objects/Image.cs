using ClipTweet.Utilities;
using System.Linq;
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

        public bool Equals(BitmapSource i)
        {
            if (this.Source == null || i == null)
            {
                return false;
            }

            return this.Source.ToBytes().SequenceEqual(i.ToBytes());
        }
    }
}