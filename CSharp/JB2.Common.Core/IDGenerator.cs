using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public static class NewID
    {
        //http://www.anotherchris.net/csharp/friendly-unique-id-generation-part-2/

        public static string ShortGuid()
        {
            return ShortGuid("{0}");
        }
        public static string ShortGuid(string wrapper)
        {
            return wrapit(wrapper, JB2.Common.ShortGuid.NewGuid());
            
        }

        public static string Guid()
        {
            return Guid("{0}");
        }

        public static string Guid(string wrapper)
        {
            return wrapit(wrapper, System.Guid.NewGuid().ToString());
            
        }


        public static string TimeHash()
        {
            return TimeHash("{0}");
        }

        public static string TimeHash(string wrapper)
        {
            long ms = DateTime.Now.Second;
            long ms2 = DateTime.Now.Millisecond;
            var t =  string.Format("{0:X}{1:X}", ms, ms2).ToLower();

            return wrapit(wrapper, t);

        }

        public static string TickHash()
        {
            return TickHash(null);
        }
        
        public static string TickHash(string wrapper)
        {
            long ticks = DateTime.Now.Ticks;
            string t = string.Format("{0:X}", ticks).ToLower();
            return wrapit(wrapper,t);
        }


        public static string UriHash(Uri u)
        {
            return UriHash("{0}", u);
        }
        public static string UriHash(string wrapper, Uri u)
        {
            int hashcode = u.GetHashCode();
            var t = string.Format("{0:X}", hashcode).ToLower();

            return wrapit(wrapper, t);
        }


        public static string Base62()
        {
            return Base62("{0}");
        }
        public static string Base62(string wrapper)
        {
            int random = ThreadSafeRandom.ThisThreadsRandom.Next();
            var t =  base62ToString(random);

            return wrapit(wrapper, t);
        }



        private static string wrapit(string w, string id)
        {
            if (String.IsNullOrEmpty(w))
                w = "{0}";
            return string.Format(w, id);
        }


        private static string base62ToString(long value)
        {
            // Divides the number by 64, so how many 64s are in
            // 'value'. This number is stored in Y.
            // e.g #1
            // 1) 1000 / 62 = 16, plus 8 remainder (stored in x).
            // 2) 16 / 62 = 0, remainder 16
            // 3) 16, 8 or G8:
            // 4) 65 is A, add 6 to this = 71 or G.
            //
            // e.g #2:
            // 1) 10000 / 62 = 161, remainder 18
            // 2) 161 / 62 = 2, remainder 37
            // 3) 2 / 62 = 0, remainder 2
            // 4) 2, 37, 18, or 2,b,I:
            // 5) 65 is A, add 27 to this (minus 10 from 37 as these are digits) = 92.
            //    Add 6 to 92, as 91-96 are symbols. 98 is b.
            // 6)
            long x = 0;
            long y = Math.DivRem(value, 62, out x);
            if (y > 0)
                return base62ToString(y) + valToChar(x).ToString();
            else
                return valToChar(x).ToString();
        }


        private static char valToChar(long value)
        {
            if (value > 9)
            {
                int ascii = (65 + ((int)value - 10));
                if (ascii > 90)
                    ascii += 6;
                return (char)ascii;
            }
            else
                return value.ToString()[0];
        }
    }
}
