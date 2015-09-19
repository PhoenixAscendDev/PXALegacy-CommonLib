using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class Image : IImage
    {

        public Image(string url)
        {
            Url = url;
        }

        public Image()
        {

        }
        public byte[] FileContent
        {
            get;set;          
        }

        public int Height
        {
            get; set;
        }

        public string Url
        {
            get; set;
        }

        public int Width
        {
            get; set;
        }
    }
}
