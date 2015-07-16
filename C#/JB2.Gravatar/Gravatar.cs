using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2
{
    public static class Gravatar
    {
        private static readonly string _imageUrlFormat = "http://www.gravatar.com/avatar/{0}";

        public static string GetImageUrl(string emailAddress,int size,string defaultImageurl)
        {
            string result = string.Empty;
            var encoder = new System.Text.UTF8Encoding();
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var hashedBytes = md5.ComputeHash(encoder.GetBytes(emailAddress.ToLower()));
            var sb = new System.Text.StringBuilder(hashedBytes.Length * 2);

            result = string.Format(_imageUrlFormat, sb);

            if( !string.IsNullOrEmpty(defaultImageurl) || size > 0)
                result += "?";

            if(size > 0 )
                result += "s=" + size.ToString();
            if(!string.IsNullOrEmpty(defaultImageurl))
                result += "d=" + System.Net.WebUtility.UrlEncode(defaultImageurl);



            return result;
        }
    }
}
