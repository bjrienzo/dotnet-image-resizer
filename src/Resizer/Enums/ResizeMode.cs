using System;
using System.Collections.Generic;
using System.Text;

namespace PeculiarCode.DotNetImageResizer.Enums
{
    /// <summary>
    /// Cover - Image will be resized, maintaining aspect ratio, so the entire canvas is filled
    /// Contain - Image will be rseized, maintaining aspect ratio, until any dimension reaches a boundary
    /// Stretch - Image will be resized, ignoring aspect ratio, to the exact supplied dimensions
    /// </summary>
    public enum ResizeMode
    {
        Cover = 1,
        Contain = 2,
        Stretch = 3
    }
}
