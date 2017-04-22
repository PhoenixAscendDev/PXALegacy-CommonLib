using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Security.Cryptography;

namespace JB2.Common
{
    public static class Hash
    {
        public static uint Adler32(string stringToHash)
        {
            byte[] sMsgBytes = Encoding.Unicode.GetBytes(stringToHash);

            return Adler32(sMsgBytes, 0, sMsgBytes.Length);
        }
        public static uint Adler32(byte[] bytesArray, int byteStart, int bytesToRead, uint checksum = 1)
        {
            int n;
            uint s1 = checksum & 0xFFFF;
            uint s2 = checksum >> 16;

            while (bytesToRead > 0)
            {
                n = (3800 > bytesToRead) ? bytesToRead : 3800;
                bytesToRead -= n;

                while (--n >= 0)
                {
                    s1 = s1 + (uint)(bytesArray[byteStart++] & 0xFF);
                    s2 = s2 + s1;
                }

                s1 %= 65521;
                s2 %= 65521;
            }

            checksum = (s2 << 16) | s1;
            return checksum;
        }


        public static byte[] MD5(string stringToHash)
        {
            byte[] sMsgBytes = Encoding.Unicode.GetBytes(stringToHash);

            return MD5(sMsgBytes);
        }

        public static byte[] MD5(byte[] data)
        {
            using (MD5 md5Hash = System.Security.Cryptography.MD5.Create())
            {
                byte[] hashdata = md5Hash.ComputeHash(data);

                return hashdata;

            }
           
        }

        public static string HashString(string stringToHash, Enum.HashType hashtype)
        {
            switch (hashtype)
            {
                case Enum.HashType.Adler32:
                    ulong hashnumber = Adler32(stringToHash);
                    return string.Format("{0:X}", hashnumber); // hashnumber.ToString("X2");
                case Enum.HashType.MD5:
                    var hash = MD5(stringToHash);
                    StringBuilder sBuilder = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                    {
                        sBuilder.Append(hash[i].ToString("x2"));
                    }
                    return sBuilder.ToString();
                case Enum.HashType.NetHashCode:
                default:
                   int num =  stringToHash.GetHashCode();
                    return string.Format("{0:X}", num); //.ToString("X2");
            }
        }
    }
}
