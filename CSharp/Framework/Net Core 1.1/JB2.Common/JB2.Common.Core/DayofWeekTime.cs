using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public struct DayOfWeekTime
    {
        #region Fields

        private System.DayOfWeek _dayofWeek;
        private TimeSpan _time;

        #endregion Fields

        #region Constructor

        public DayOfWeekTime(DateTime dt) : this( dt.DayOfWeek,dt.TimeOfDay)
        {

        }

        public DayOfWeekTime(System.DayOfWeek dayofWeek, TimeSpan time)
        {
            _dayofWeek = dayofWeek;
            _time = time;
        }

        #endregion Constructor


        #region Properties
        public System.DayOfWeek DayOfWeek
        {
            get
            {
                return _dayofWeek;
            }
        }

        public TimeSpan Time
        {
            get
            {
                return _time;
            }
        }

        #endregion Properties



    }
}
