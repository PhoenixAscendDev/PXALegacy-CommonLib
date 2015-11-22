using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface IImage
    {
        string Url { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        byte[] FileContent { get; set; }
    }
}
