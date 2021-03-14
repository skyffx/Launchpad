using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Launchpad.Utils
{
    public static class ImageUtil
    {
        public static byte[] ResizeImageFromStream(byte[] data, int width, int height)
        {
            using (var stream = new MemoryStream(data))
            {
                var image = Image.FromStream(stream);
                var thumbnail = image.GetThumbnailImage(width, height, null, IntPtr.Zero);
                using (var thumbnailStream = new MemoryStream())
                {
                    thumbnail.Save(thumbnailStream, ImageFormat.Png);
                    return thumbnailStream.ToArray();
                }
            }
        }
        
        public static Image ResizeImageAndKeepRatio(Image image, int imageWidth, int imageHeight)
        {
            var ratioX = (double)imageWidth / image.Width;
            var ratioY = (double)imageHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }
    }
}