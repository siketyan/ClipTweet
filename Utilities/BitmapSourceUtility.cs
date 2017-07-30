using System.IO;
using System.Windows.Media.Imaging;

namespace ClipTweet.Utilities
{
    public static class BitmapSourceUtility
    {
        public static byte[] ToBytes(this BitmapSource img)
        {
            var data = new byte[] { };
            if (img != null)
            {
                try
                {
                    var encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(img));
                    using (var stream = new MemoryStream())
                    {
                        encoder.Save(stream);
                        data = stream.ToArray();
                    }
                    return data;
                }
                catch
                {
                    return null;
                }
            }
            return data;
        }
    }
}