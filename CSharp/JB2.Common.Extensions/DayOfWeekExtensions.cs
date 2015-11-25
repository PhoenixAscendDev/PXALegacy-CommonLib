using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public static class DayOfWeekExtensions
    {
        public static DayOfWeekTime ToDayOfWeekTime(this DateTime dt)
        {
            return new DayOfWeekTime(dt);
        }
    }
}
