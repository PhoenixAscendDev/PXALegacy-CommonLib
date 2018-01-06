using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Common
{
    public struct DayofWeekTimeRange
    {
        #region Fields

        private System.DayOfWeek _dayofWeek;
        private TimeRange _timeRange;
       

        #endregion Fields

        #region Constructor
        public DayofWeekTimeRange(System.DayOfWeek dayofWeek, TimeSpan startTime, TimeSpan endTime)
        {
            _dayofWeek = dayofWeek;
            _timeRange = new TimeRange(startTime, endTime);
        }

        #endregion Constructor

        #region Properties

        public DayOfWeek DayOfWeek
        {
            get
            {
                return _dayofWeek;
            }
            set
            {
                _dayofWeek = value;
            }
        }

        public TimeRange TimeRange
        {
            get
            {
                return _timeRange;
            }
            set
            {
                _timeRange = value;
            }

        }

        #endregion Properties

        #region Method

        public bool IsWithinRange(DayOfWeek dayofWeek, TimeSpan timeofDay)
        {
            return ((_dayofWeek == dayofWeek) && (_timeRange.IsWithinRange(timeofDay)));
            
        }

        public bool IsWithinRange(DateTime dt)
        {
            return IsWithinRange(dt.DayOfWeek, dt.TimeOfDay);
        }

        #endregion Method

        #region Implicit Operators

        public static implicit operator string(DayofWeekTimeRange r)
        {
            return string.Format("{0}:{1}:{2}", ((int)r._dayofWeek).ToString(), r._timeRange.Min.Ticks.ToString(), r._timeRange.Max.Ticks.ToString());
        }

        public static implicit operator DayofWeekTimeRange(string s)
        {
            DayofWeekTimeRange result = null;
            if(ValidCode(s))
            {
                string[] values = s.Split(':');
                result = new DayofWeekTimeRange((DayOfWeek)Convert.ToInt16(values[0]),
                                                new TimeSpan(Convert.ToInt64(values[1])),
                                                new TimeSpan(Convert.ToInt64(values[2])));
            }
            return result;
        }

        #endregion Implicit Operators


        #region ToString

        public override string ToString()
        {
            return (string)this;
        }

        #endregion ToString

        #region Static Constructors

        public static ServiceResult ValidCode(string code)
        {
            return true;
        }

        public static DayofWeekTimeRange FromString(string s)
        {
            return (DayofWeekTimeRange)s;
        }

        #endregion Static Constructors
    }
}
