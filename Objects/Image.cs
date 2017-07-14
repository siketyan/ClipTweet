using System.IO;
using System.Windows.Media.Imaging;
using Drawing = System.Drawing;

namespace ClipTweet.Objects
{
    public class Image
    {
        public BitmapImage Source { get; set; }

        public Image(Drawing.Image src)
        {
            using (var stream = new MemoryStream())
            {
                src.Save(stream, src.RawFormat);
                stream.Position = 0;

                this.Source = new BitmapImage();
                this.Source.BeginInit();
                this.Source.CacheOption = BitmapCacheOption.OnLoad;
                this.Source.StreamSource = stream;
                this.Source.EndInit();
            }
        }
    }
}