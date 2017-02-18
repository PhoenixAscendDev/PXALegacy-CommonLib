using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public abstract class Range<T> : IRange<T>
        where T : IComparable
    {
        protected T _min;
        protected T _max;


        public T Min
        {
          get { return _min; }
          set { _min = value; }
        }

        public T Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public abstract bool IsWithinRange(T value);

    }
}
