using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public static class JB2DateExtensions
    {
        public static int ToJB2DateKey(this DateTime dt)
        {
            return ((JB2Date)dt).DateKey;
        }

        public static JB2Date ToJB2Date(this DateTime dt)
        {
            return (JB2Date)dt;
        }

        public static bool IsValidJB2DateKey(this int datekey)
        {
            try
            {
                var dt = new JB2Date(datekey);
                return dt.IsValidKey(datekey);
            }
            catch
            {
                return false;
            }
        }

    }
}
