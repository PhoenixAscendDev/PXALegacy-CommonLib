using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2
{
    public static class Gravatar
    {
        private static readonly string _imageUrlFormat = "http://www.gravatar.com/avatar/{0}";

        public static string GetImageUrlByEmail(string emailAddress,int size,string defaultImageurl)
        {
            
            var encoder = new System.Text.UTF8Encoding();
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var hashedBytes = md5.ComputeHash(encoder.GetBytes(emailAddress.ToLower()));
            var emailHash = hashedBytes.ToHex(false);
            return GetImageUrl(emailHash, size, defaultImageurl);
        }

        public static string GetImageUrl(string emailHash, int size, string defaultImageurl)
        {
            string result = string.Empty;
            var sb = new System.Text.StringBuilder();
            sb.Append(emailHash);           
            result = string.Format(_imageUrlFormat, sb.ToString());

            //if( !string.IsNullOrEmpty(defaultImageurl) || size > 0)
            result += "?";

            if (size > 0)
                result += "s=" + size.ToString();
            else
                result += "s=180";
            if (!string.IsNullOrEmpty(defaultImageurl))
                result += "&d=" + System.Net.WebUtility.UrlEncode(defaultImageurl);

            //return sb.ToString();
            return result;
        }
    }
}