using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace PeculiarCode.DotNetImageResizer.Tests
{
    public class ImageResizerTests
    {

        /// <summary>
        /// Shrink an image down, using the 'Stretch' method, end result should exactly match inputs
        /// </summary>
        [Fact]
        public void ShrinkImageStretchSuccess()
        {

            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);
               
            var shrunkImage = PeculiarCode.DotNetImageResizer.Utility.Resizer.ResizeImage(Path.Combine(dirPath, "Resources\\TestImageLarge.jpg"), 250,200, Enums.ResizeMode.Stretch);

            Assert.Equal(250, shrunkImage.Width);
            Assert.Equal(200, shrunkImage.Height);

        }

        /// <summary>
        /// Shrink an image down, using the 'Cover' method, end result should exactly match inputs
        /// </summary>
        [Fact]
        public void ShrinkImageCoverSuccess()
        {

            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);


            var shrunkImage = PeculiarCode.DotNetImageResizer.Utility.Resizer.ResizeImage(Path.Combine(dirPath, "Resources\\TestImageLarge.jpg"), 250, 200, Enums.ResizeMode.Cover);

            Assert.Equal(250, shrunkImage.Width);
            Assert.Equal(200, shrunkImage.Height);

        }


        /// <summary>
        /// Shrink an image down, using the 'Contain' method, end result should maintain original ratio and not exceed either input
        /// </summary>
        [Fact]
        public void ShrinkImageContainSuccess()
        {

            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);


            var shrunkImage = PeculiarCode.DotNetImageResizer.Utility.Resizer.ResizeImage(Path.Combine(dirPath, "Resources\\TestImageLarge.jpg"), 250, 200, Enums.ResizeMode.Contain);

            Assert.Equal(250, shrunkImage.Width);
            Assert.Equal(187, shrunkImage.Height);

        }

        /// <summary>
        /// Enllarge an image, using the 'Stretch' method, end result should exactly match inputs
        /// </summary>
        [Fact]
        public void StretchImageStretchSuccess()
        {

            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);


            var shrunkImage = PeculiarCode.DotNetImageResizer.Utility.Resizer.ResizeImage(Path.Combine(dirPath, "Resources\\TestImageSmall.jpg"), 1920, 1080, Enums.ResizeMode.Stretch);

            Assert.Equal(1920, shrunkImage.Width);
            Assert.Equal(1080, shrunkImage.Height);

        }

        /// <summary>
        /// Enlarge an image, using the 'Cover' method, end result should exactly match inputs
        /// </summary>
        [Fact]
        public void StretchImageCoverSuccess()
        {

            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);


            var shrunkImage = PeculiarCode.DotNetImageResizer.Utility.Resizer.ResizeImage(Path.Combine(dirPath, "Resources\\TestImageSmall.jpg"), 1920, 1080, Enums.ResizeMode.Cover);

            Assert.Equal(1920, shrunkImage.Width);
            Assert.Equal(1080, shrunkImage.Height);

        }


        /// <summary>
        /// Enlarge an image, using the 'Contain' method, end result should maintain original ratio and not exceed either input
        /// </summary>
        [Fact]
        public void StretchImageContainSuccess()
        {

            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);


            var shrunkImage = PeculiarCode.DotNetImageResizer.Utility.Resizer.ResizeImage(Path.Combine(dirPath, "Resources\\TestImageSmall.jpg"), 1920, 1080, Enums.ResizeMode.Contain);

            Assert.Equal(1440, shrunkImage.Width);
            Assert.Equal(1080, shrunkImage.Height);

        }

    }
}
