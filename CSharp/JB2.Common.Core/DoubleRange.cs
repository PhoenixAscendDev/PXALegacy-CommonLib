using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class DoubleRange : Range<double>, IRange<double>
    {

        public DoubleRange(double min, double max)
        {
            _min = min;
            _max = max;
        }

        public override bool IsWithinRange(double value)
        {
            return ((value >= _min) && (value <= _max));
        }

    }
}
