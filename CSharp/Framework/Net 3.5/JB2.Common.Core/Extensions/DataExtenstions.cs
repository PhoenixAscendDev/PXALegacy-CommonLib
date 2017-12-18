using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;





namespace JB2.Common
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
        public static string[] Split(this string value, string seperator)
        {
            return System.Text.RegularExpressions.Regex.Split(value, seperator);
        }

        public static System.Collections.BitArray ToBitArray(this string s)
        {
            List<bool> marks = new List<bool>(s.Length);

            foreach (char c in s.ToCharArray())
            {
                marks.Add(c == '1' ? true : false);
            }

            return new System.Collections.BitArray(marks.ToArray());
        }

        public static string TrySplit(this string value, char seperator, int index)
        {
            return TrySplit(value, seperator.ToString(), index);
        }
        public static string TrySplit(this string value, string seperator, int index)
        {
            try
            {
                var result = System.Text.RegularExpressions.Regex.Split(value, seperator);
                return result[index];
            }
            catch
            {
                return string.Empty;
            }
        }



        public static string TrimLastCharacter(this String str)
        {

            //sourcr: http://stackoverflow.com/questions/3573284/trim-last-character-from-a-string
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }
            else
            {
                return str.TrimEnd(str[str.Length - 1]);
            }
        }

        public static Tag ToTag(this string str, string seperator = ":")
        {
            return Tag.FromString(str, seperator);
        }
    }

    public static class ObjectExtensions
    {
        public static bool IsNumber(this object value)
        {
            bool result = value is sbyte
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

            if (result)
                return result;

            //tryparse
            int numInt = 0;
            if (int.TryParse(value.ToString(), out numInt))
                return true;

            sbyte numsByte = 0;
            if (sbyte.TryParse(value.ToString(), out numsByte))
                return true;

            byte numByte = 0;
            if (byte.TryParse(value.ToString(), out numByte))
                return true;

            short numShort = 0;
            if (short.TryParse(value.ToString(), out numShort))
                return true;

            ushort numUshort = 0;
            if (ushort.TryParse(value.ToString(), out numUshort))
                return true;

            uint numUint = 0;
            if (uint.TryParse(value.ToString(), out numUint))
                return true;

            long numLong = 0;
            if (long.TryParse(value.ToString(), out numLong))
                return true;

            ulong numUlong = 0;
            if (ulong.TryParse(value.ToString(), out numUlong))
                return true;

            float numFloat = 0;
            if (float.TryParse(value.ToString(), out numFloat))
                return true;

            double numDouble = 0;
            if (double.TryParse(value.ToString(), out numDouble))
                return true;

            decimal numDecimal = 0;
            if (decimal.TryParse(value.ToString(), out numDecimal))
                return true;

            return true;

        }

        public static string ToStringOrEmpty(this object value)
        {
            return value == null ? string.Empty : value.ToString();
        }
    }

    public static class BitArrayExtensions
    {
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

        public static System.Collections.BitArray GetSubSet(this System.Collections.BitArray bits, int index, int length)
        {
            bool[] result = new bool[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = bits[index + i];
            }
            return new System.Collections.BitArray(result);
        }

        public static T ToNumber<T>(this System.Collections.BitArray b)
        {

            object result = 0;

            if (typeof(T) == typeof(ulong))
            {
                var array = new byte[8];
                b.CopyTo(array, 0);

                result = BitConverter.ToUInt64(array, 0);
            }

            else if (typeof(T) == typeof(long))
            {
                var array = new byte[8];
                b.CopyTo(array, 0);

                result = BitConverter.ToInt64(array, 0);
            }

            else if (typeof(T) == typeof(uint))
            {
                var array = new byte[4];
                b.CopyTo(array, 0);

                result = BitConverter.ToUInt32(array, 0);
            }



            if (typeof(T) == typeof(int))
            {
                var array = new byte[4];
                b.CopyTo(array, 0);

                result = BitConverter.ToInt32(array, 0);
            }


            else if (typeof(T) == typeof(ushort))
            {
                var array = new byte[2];
                b.CopyTo(array, 0);

                result = BitConverter.ToUInt16(array, 0);
            }

            else if (typeof(T) == typeof(short))
            {
                var array = new byte[2];
                b.CopyTo(array, 0);

                result = BitConverter.ToInt16(array, 0);
            }

            return (T)Convert.ChangeType(result, typeof(T));
        }

        public static void Fill(this System.Collections.BitArray bits, bool fillWith)
        {
            for (int i = 0; i < bits.Length; i++)
            {
                bits[i] = fillWith;
            }
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


    }


    public static class NumberExtensions
    {
        public static BitArray ToBitArray(this int number, int size = 32)
        {
            //string s = Convert.ToString(number, 2);

            bool[] bits = Convert.ToString((int)number, 2).PadLeft(size, '0').Select(s => s.Equals('1')).ToArray();

            Array.Reverse(bits);

            BitArray result = new BitArray(bits);

            return result;
        }

        public static BitArray ToBitArray(this byte number, int size = 8)
        {
            //string s = Convert.ToString(number, 2);

            bool[] bits = Convert.ToString((byte)number, 2).PadLeft(size, '0').Select(s => s.Equals('1')).ToArray();

            Array.Reverse(bits);

            BitArray result = new BitArray(bits);

            return result;
        }

        public static BitArray ToBitArray(this short number, int size = 16)
        {
            //string s = Convert.ToString(number, 2);

            bool[] bits = Convert.ToString((short)number, 2).PadLeft(size, '0').Select(s => s.Equals('1')).ToArray();

            Array.Reverse(bits);

            BitArray result = new BitArray(bits);

            return result;
        }

        public static BitArray ToBitArray(this ushort number, int size = 16)
        {
            return ((short)number).ToBitArray(size);
        }

        public static BitArray ToBitArray(this long number, int size = 64)
        {
            //string s = Convert.ToString(number, 2);

            bool[] bits = Convert.ToString((long)number, 2).PadLeft(size, '0').Select(s => s.Equals('1')).ToArray();

            Array.Reverse(bits);

            BitArray result = new BitArray(bits);

            return result;
        }

        public static BitArray ToBitArray(this ulong number, int size = 16)
        {
            return ((long)number).ToBitArray(size);
        }




    }


    public static class EnumExtensions
    {
        public static T GetAttributeOfType<T>(this System.Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

    }
}
