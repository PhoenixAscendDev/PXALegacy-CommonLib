using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JB2.Common
{
    public static class Utility
    {
        /// <summary>
        /// 2010.04.12 (JB) Use this utility when you do not want to display the full value of a string.
        /// It will only display the first [maxLength] number of chars that you tell it
        /// If the text is longer then the [maxLength] it will cut off and append the [trailText] to the end
        /// Helpful in a Gridview Column when space is a concern
        /// e.g ShortText("abcefghijk this is a long text value",6,"...")  will return "abcdefg..."
        /// e.g ShortText("abcefghijk this is a long text value",13,"") will return "abcefghijk th"
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLength"></param>
        /// <param name="trailText"></param>
        /// <returns></returns>
        /// 
        public static string ShortText(string text, int maxLength, string trailText)
        {
            string result = string.Empty;
            if (!String.IsNullOrEmpty(result) && result.Length >= maxLength)
                result = text.Substring(0, maxLength) + trailText;
            else
                result = text;
            return result;
        }


        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }


        //public static string GetDescription(this System.Enum value)
        //{
        //    string result = value.ToString();
        //    var fieldInfo = value.GetType().GetField(result);
        //    var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        //    if (attributes != null && attributes.Length > 0)
        //    {
        //        result = attributes[0].Description;
        //    }

        //    return result;
        //}
    }
}
