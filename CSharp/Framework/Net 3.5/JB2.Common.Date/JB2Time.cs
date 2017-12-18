using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Globalization;

using System.Reflection;

namespace JB2.Common
{
    public class JB2Time
    {
        protected int _timekey;
        protected DateTime? _dt;
        static readonly int MINKEY = 1000000;
        static readonly int MAXKEY = 1246060;
        const int TOTAL_SEC_IN_DAY = 86400;
        const int TOTAL_MIN_IN_DAY = 1440;
        const int TOTAL_HOUR_IN_DAY = 24;

        public JB2Time(int timekey)
        {
            if (!IsValidKey(timekey))
                _timekey = MINKEY;
            else
                _timekey = timekey;

            var keyString = keyToString(_timekey);
            string hour = keyString.Substring(1, 2);
            string min = keyString.Substring(3, 2);
            string sec = keyString.Substring(5, 2);

            _dt = new DateTime(2017, 01, 01, Convert.ToInt32(hour), Convert.ToInt32(min), Convert.ToInt32(sec));

        }

        public int TimeKey
        {
            get
            {
                return _timekey;
            }
        }
        public int TimeAltKey
        {
            get
            {
                return this.SecondOfDay;
            }
        }

        public int Hour
        {
            get
            {
                return _dt.Value.Hour;
            }
        }

        public int Minute
        {
            get
            {
                return _dt.Value.Minute;
            }
        }

        public int Second
        {
            get
            {
                return _dt.Value.Second;
            }
        }

        public int HourOfDay12
        {
            get
            {
                return Convert.ToInt32(_dt.Value.ToString("hh"));
            }
        }

        public int HourOfDay24
        {
            get
            {
                return Convert.ToInt32(_dt.Value.ToString("HH"));
            }
        }

        public int MinuteOfDay
        {
            get
            {
                var ts = _dt.Value.TimeOfDay;

                return Convert.ToInt32(ts.TotalMinutes);
            }
        }

        public int MinuteOfHour
        {
            get
            {
                return Minute;
            }
        }

        public int SecondOfDay
        {
            get
            {
                var ts = _dt.Value.TimeOfDay;

                return Convert.ToInt32(ts.TotalSeconds);
            }
        }

        public int SecondOfHour
        {
            get
            {
                var min = this.MinuteOfHour;

                return (min * 60) + this.Second;
            }
        }

        public int SecondOfMinute
        {
            get
            {
                return this.Second;
            }
        }



        public TimeSpan TimeOfDay
        {
            get
            {
                return _dt.Value.TimeOfDay;
            }
        }

        public string AmPm
        {
            get
            {
                return _dt.Value.ToString("tt");
            }
        }

        public int HalfHour
        {
            get
            {
                return ((this.SecondOfDay / 30) % 2) + 1;
            }
        }

        public int HalfHourOfDay
        {
            get
            {
                return (this.SecondOfDay / 30) + 1;
            }
        }

        public int QuarterHour
        {
            get
            {
                return ((this.SecondOfDay / 15) % 4) + 1;
            }
        }

        public int QuarterHourOfDay
        {
            get
            {
                return (this.SecondOfDay / 15) + 1;
            }
        }

        public string TimeMinUSA
        {
            get
            {
                return string.Format("{0:hh:mm tt}", _dt.Value);
            }
        }

        public string TimeMinUSASansPeriod
        {
            get
            {
                return string.Format("{0:hh:mm}", _dt.Value);
            }
        }
        public string TimeSecUSA
        {
            get
            {
                return string.Format("{0:hh:mm:ss tt}", _dt.Value);
            }
        }

        public string TimeSecUSASansPeriod
        {
            get
            {
                return string.Format("{0:hh:mm:ss}", _dt.Value);
            }
        }



        public string TimeMinUSA24
        {
            get
            {
                return string.Format("{0:HH:mm}", _dt.Value);
            }
        }

        public string TimeSecUSA24
        {
            get
            {
                return string.Format("{0:HH:mm:ss}", _dt.Value);
            }
        }

        public string TimeMilitaryUSA
        {
            get
            {
                var hourpart = String.Format("{0:D2}", this.HourOfDay24);
                var minutepart = String.Format("{0:D2}", this.Minute);
                return hourpart + minutepart;
            }
        }

        public string DayNight
        {
            get
            {
                return (this.HourOfDay24 >= 6 && this.HourOfDay24 < 18) ? "Day" : "Night";
            }
        }

        public string DaytimeName
        {
            get
            {
                var hour = this.HourOfDay24;

                switch (hour)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return "Night";
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        return "Morning";
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        return "Afternoon";
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                        return "Evening";
                    case 22:
                    case 23:
                    case 24:
                    case 0:
                    default:
                        return "Night";
                }
            }
        }






















        #region Static


        public static JB2Time MinTime()
        {
            return new JB2Time(MINKEY);
        }

        public static JB2Time MaxTime()
        {
            return new JB2Time(MAXKEY);
        }


        public static JB2Time FromDateTime(DateTime dt)
        {

            return FromSecondsInDay(Convert.ToInt32(dt.TimeOfDay.TotalSeconds));
        }

        public static JB2Time FromHourInDay(int hours)
        {
            if (hours > JB2Time.TOTAL_HOUR_IN_DAY)
                hours = JB2Time.TOTAL_HOUR_IN_DAY;

            return FromMinutesInDay(hours * 60);
        }

        public static JB2Time FromMinutesInDay(int minutes)
        {
            if (minutes > JB2Time.TOTAL_MIN_IN_DAY)
                minutes = JB2Time.TOTAL_MIN_IN_DAY;
            return FromSecondsInDay(minutes * 60);
        }
        public static JB2Time FromSecondsInDay(int seconds)
        {
            try
            {

                if (seconds > JB2Time.TOTAL_SEC_IN_DAY)
                    seconds = JB2Time.TOTAL_SEC_IN_DAY;
                var ts = TimeSpan.FromSeconds(Convert.ToDouble(seconds));

                var hours = ts.Hours * 10000;
                var minutes = ts.Minutes * 100;
                var secs = ts.Seconds;

                var key = (1000000 + hours + minutes + secs);

                return new JB2Time(key);
            }
            catch (Exception ex)
            {
                return JB2Time.MinTime();
            }

        }

        public static JB2Time FromTimePart(int hour = 0, int minute = 0, int seconds = 0)
        {
            var totalminutes = (hour * 60) + minute;
            var totalsecs = (totalminutes * 60) + seconds;

            return FromSecondsInDay(totalsecs);
        }

        protected static string keyToString(int key)
        {
            return String.Format("{0:D7}", key);
        }
        public static bool IsValidKey(int key)
        {
            bool result = true;
            if (key < MINKEY || key > MAXKEY)
                return false;

            var keyString = keyToString(key);

            try
            {

                string hour = keyString.Substring(1, 2);
                string min = keyString.Substring(3, 2);
                string sec = keyString.Substring(5, 2);

                string strDate = "20150101 " + hour + min + sec; //new DateTime(2015, 1, 1, Convert.ToInt32(hour), Convert.ToInt32(min), Convert.ToInt32(sec)).ToString();
                System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.DateTimeFormatInfo();
                dtfi.ShortDatePattern = "yyyyMMdd";
                dtfi.ShortTimePattern = "HHmmss";
                dtfi.DateSeparator = "";
                DateTime objDate = DateTime.ParseExact(
                strDate,
                "yyyyMMdd HHmmss",
                CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                return false;
            }

            return result;
        }

        #endregion Static


        #region To Convert Methods


        public TimeSpan ToTimeSpan()
        {
            return _dt.Value.TimeOfDay;
        }

        public DateTime ToDateTime()
        {
            return _dt.GetValueOrDefault(DateTime.MinValue);
        }

        public int ToInt()
        {
            return _timekey;
        }

        public override string ToString()
        {
            return _dt.Value.ToShortTimeString();
        }

        #endregion To Convert Methods


        #region implicit operators

        public static implicit operator DateTime(JB2Time rhs)
        {
            if (rhs == null)
                return JB2Time.MinTime().ToDateTime();

            return rhs.ToDateTime();
        }

        public static implicit operator TimeSpan(JB2Time rhs)
        {
            if (rhs == null)
                return JB2Time.MinTime().ToTimeSpan();

            return rhs.ToTimeSpan();
        }



        public static implicit operator int(JB2Time rhs)
        {
            return rhs.ToInt();
        }

        public static implicit operator JB2Time(int rhs)
        {
            return new JB2Time(rhs);
        }

        public static implicit operator JB2Time(DateTime dt)
        {
            return JB2Time.FromDateTime(dt);
        }

        #endregion implicit operators
    }
}
