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

    public static class JB2Extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
