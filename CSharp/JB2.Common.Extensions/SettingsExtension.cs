using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    
    public static class ShortGuidExtension
    {
        public static string NewShortGuid(this string str)
        {
            return (string)JB2.Common.ShortGuid.NewGuid();
        }

    }
}
