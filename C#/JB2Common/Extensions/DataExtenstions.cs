using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common.Extensions
{
    public static class DataExtenstions
    {
        public static string ToHex(this byte[] bytes, bool upperCase)
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }
    }
}
