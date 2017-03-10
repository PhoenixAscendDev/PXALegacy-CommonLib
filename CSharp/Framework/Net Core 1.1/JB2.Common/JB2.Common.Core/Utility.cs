using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

using System.Reflection;

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
        public static T GetAttributeOfType<T>(this System.Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetTypeInfo().GetMembers();
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Count() > 0) ? (T)attributes.ToArray()[0] : null;
        }
    }
}
