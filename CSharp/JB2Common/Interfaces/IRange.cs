using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IRange<T>
         where T : IComparable
    {
        T Min { get; set; }
        T Max { get; set; }

        bool IsWithinRange(T value);
    }
}
