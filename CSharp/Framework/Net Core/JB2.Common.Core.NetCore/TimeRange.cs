using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class TimeRange : IRange<TimeSpan>
    {
        #region Fields
        private TimeSpan _min;
        private TimeSpan _max;
        #endregion Fields

        #region Contructors

        public TimeRange(TimeSpan min, TimeSpan max)
        {
            _min = min;
            _max = max;

            if (_max < _min)
            {
                var temp = _max;
                _max = _min;
                _min = _max;
            }
        }

        #endregion Constructors

        #region Propoerties

        public TimeSpan Min
        {
            get { return _min; }
            set { _min = value; }
           
        }

        public TimeSpan Max
        {

            get { return _max; }
            set { _max = value; }
        }

        #endregion Properties


        public bool IsWithinRange(TimeSpan value)
        {
            return ((value > _min) && (value < _max));           
        }

        public bool IsWithinRange(DateTime dt)
        {
            return IsWithinRange(dt.TimeOfDay);
        }

        





    }
}
