using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using JB2.Common.Extensions;
namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            //JB2.Gravatar.GetImageUrlByEmail("bethiemoss@yahoo.com");

            string emailAddress = "bethiemoss@yahoo.com";
            var encoder = new System.Text.UTF8Encoding();
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var hashedBytes = md5.ComputeHash(encoder.GetBytes(emailAddress.ToLower()));
            var emailHash = hashedBytes.ToHex(false);
            //return GetImageUrl(emailHash, size, defaultImageurl);
        }
    }
}
