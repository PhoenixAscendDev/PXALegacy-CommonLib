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

    public static class ListExtenstions
    {
        public static void Move<T>(this IList<T> list, int iIndexToMove,
        Enum.ElevatorDirection direction)
        {

            if (direction == Enum.ElevatorDirection.Up)
            {
                var old = list[iIndexToMove - 1];
                list[iIndexToMove - 1] = list[iIndexToMove];
                list[iIndexToMove] = old;
            }
            else
            {
                var old = list[iIndexToMove + 1];
                list[iIndexToMove + 1] = list[iIndexToMove];
                list[iIndexToMove] = old;
            }
        }


    }

    public static class StringExtenstions
    {
        public static string[] Split(this string value, string seperator )
        {
            return System.Text.RegularExpressions.Regex.Split(value, ">*<");
        }

        public static System.Collections.BitArray ToBitArray(this string s)
        {
            List<bool> marks = new List<bool>(s.Length);

            foreach(char c in s.ToCharArray())
            {
                marks.Add(c == '1' ? true : false);
            }

            return new System.Collections.BitArray(marks.ToArray());
        }
    }

    public static class ObjectExtensions
    {
        public static bool IsNumber(this object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }
    }

    public static class JB2Extensions
    {

        public static void Shuffle2<T>(this IEnumerable<T> source)
        {

            source = source.ShuffleIterator<T>();

        }

        private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source)
        {
            var buffer = source.ToList();
            int n = buffer.Count();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }

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

        public static string ToBitString(this System.Collections.BitArray bits)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < bits.Count; i++)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
