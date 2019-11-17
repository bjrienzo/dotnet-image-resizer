using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

namespace PeculiarCode.DotNetImageResizer.Utility
{
    public sealed class Resizer
    {
        /// <summary>
        /// Resize a source image to the target dimensions, based on the resize mode
        /// </summary>
        /// <param name="fileName">Full path to the Image to Resize</param>
        /// <param name="width">Target desired Width</param>
        /// <param name="height">Target desired Height</param>
        /// <param name="mode">Resize Mode</param>
        /// <returns>Resized Image</returns>
        public static Bitmap ResizeImage(string fileName, int width, int height, Enums.ResizeMode mode)
        {

            if (string.IsNullOrWhiteSpace(fileName)) { throw new ArgumentNullException(nameof(fileName)); }

            if (!System.IO.File.Exists(fileName)) { throw new ArgumentException("File does not exist", nameof(fileName)); }

            Image srcImage;
            try
            {
                srcImage = Image.FromFile(fileName);
            }
            catch (OutOfMemoryException)
            {
                //Image.FromFile throws OutOfMemoryException when the file is invalid or unsupported
                throw new ArgumentException("File type is not supported", nameof(fileName));
            }

            return ResizeImage(srcImage, width, height, mode);

        }

        /// <summary>
        /// Resize a source image to the target dimensions, based on the resize mode
        /// </summary>
        /// <param name="srcImage">Image to Resize</param>
        /// <param name="width">Target desired Width</param>
        /// <param name="height">Target desired Height</param>
        /// <param name="mode">Resize Mode</param>
        /// <returns>Resized Image</returns>
        public static Bitmap ResizeImage(Image srcImage, int width, int height, Enums.ResizeMode mode)
        {

            if (srcImage == null) { throw new ArgumentNullException(nameof(srcImage)); }

            if (width <= 0) { throw new ArgumentException("Width must be Greater than Zero", nameof(width)); }

            if (height <= 0) { throw new ArgumentException("Height must be Greater than Zero", nameof(height)); }

            Rectangle destRect;
            Bitmap destImage;

            int xPos = 0;
            int yPos = 0;
            int cropHeight = srcImage.Height;
            int cropWidth = srcImage.Width;
            var widthRatio = (double)width / srcImage.Width;
            var heightRatio = (double)height / srcImage.Height;
            double ratio;

            switch (mode)
            {
                //Resize image with no regard for life or limb
                case Enums.ResizeMode.Stretch:
                    destRect = new Rectangle(0, 0, width, height);
                    destImage = new Bitmap(width, height);
                    break;

                //Resize the image as large as possible while maintaining aspect ratio and not exceeding the bounds
                case Enums.ResizeMode.Contain:

                    ratio = widthRatio < heightRatio ? widthRatio : heightRatio;
                    destRect = new Rectangle(0, 0, (int)(srcImage.Width * ratio), (int)(srcImage.Height * ratio));
                    destImage = new Bitmap((int)(srcImage.Width * ratio), (int)(srcImage.Height * ratio));
                    break;

                //Completely cover the canvas, while maintaining aspect ratio
                case Enums.ResizeMode.Cover:
                    ratio = widthRatio > heightRatio ? widthRatio : heightRatio;

                    //Destination image is exactly requested dimensions
                    destRect = new Rectangle(0, 0, width, height);
                    destImage = new Bitmap(width, height);

                    //Scale the source image so completely covers the requested dimensions
                    var scaledX = Convert.ToInt32(srcImage.Width * ratio);
                    var scaledY = Convert.ToInt32(srcImage.Height * ratio);

                    //Set the start coordinates, centering the source image in the target
                    xPos = (int)((double)(scaledX - destRect.Width) / 2 / ratio);
                    yPos = (int)((double)(scaledY - destRect.Height) / 2 / ratio);

                    //Set the dimensions to grab from the original image
                    cropHeight = srcImage.Height - Convert.ToInt32((scaledY - height) / ratio);
                    cropWidth = srcImage.Width - Convert.ToInt32((scaledX - width) / ratio);
                    break;

                default:
                    throw new ArgumentException("Invalid Resize Mode", nameof(mode));
            }

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(srcImage, destRect, xPos, yPos, cropWidth, cropHeight, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
