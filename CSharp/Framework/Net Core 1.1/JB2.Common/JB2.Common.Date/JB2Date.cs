using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;

using System.Reflection;

namespace JB2.Common
{
    public class JB2Date
    {
        private int _datekey;
        private string _dateStringFormat = "";
        private DateTime? _dt;
        static readonly int MINKEY = 17500101;
        static readonly int MAXKEY = 20201231;

        public JB2Date(int dateKey)
        {
            if (!IsValidKey(dateKey))
                _datekey = MINKEY;
            else
                _datekey = dateKey;
        }

        #region Public Properties

        public long Ticks
        {
            get { return ((DateTime)this).Ticks; }
        }

        public int DateKey
        {
            get
            {
                return _datekey;
            }
        }

        public string DateUSA
        {
            get
            {
                return ((DateTime)this).ToString("MM/dd/yyyy");
            }
        }


        public bool IsWeekend
        {
            get
            {
                var dt = (DateTime)this;
                return (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday);
            }
        }


        public bool IsWeekday
        {
            get
            {
                return (!IsWeekend);
            }

        }


        public string DaySuffix
        {
            get
            {
                switch (DayOfMonth)
                {
                    case 1:
                    case 21:
                    case 31:
                        return "st";
                    case 2:
                    case 22:
                        return "nd";
                    case 3:
                    case 23:
                        return "rd";
                    default:
                        return "th";
                }
            }
        }

        public string LongName
        {
            get
            {
                return ((DateTime)this).ToString("dddd, dd MMMM yyyy");
            }
        }


        public byte DayOfMonth
        {
            get
            {
                return Convert.ToByte(_datekey.ToString().Substring(6, 2));
            }
        }

        public byte DayofWeek
        {
            get
            {
                var dt = (DateTime)this;
                return (byte)dt.DayOfWeek;
            }
        }


        public byte DayofYear
        {
            get
            {
                var dt = (DateTime)this;
                return (byte)dt.DayOfYear;
            }
        }

        public string DayofWeekName
        {
            get
            {
                var dt = (DateTime)this;
                return dt.DayOfWeek.ToString();
            }
        }

        public byte Month
        {
            get
            {
                return Convert.ToByte(_datekey.ToString().Substring(4, 2));
            }
        }

        public string MonthName
        {
            get
            {
                return ((DateTime)this).ToString("MMMM");

            }
        }

        public byte MonthOfQuarter
        {
            get
            {
                return (this.Month % 3) == 0 ? (byte)1 : (byte)(this.Month % 3);
            }

        }

        public byte WeekOfMonth
        {

            get
            {
                System.Globalization.GregorianCalendar gc = new GregorianCalendar();

                var wofY = gc.GetWeekOfYear(this, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);

                var first = gc.GetWeekOfYear(new DateTime(this.Year, this.Month, 1), CalendarWeekRule.FirstDay, DayOfWeek.Sunday);

                return (byte)(wofY - first + 1);
                //return this.ToDatetime().
            }
        }

        public byte WeekOfYear
        {
            get
            {
                System.Globalization.GregorianCalendar gc = new GregorianCalendar();
                return (byte)gc.GetWeekOfYear(this, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);

            }

        }

        public short Year
        {
            get
            {
                return Convert.ToInt16(_datekey.ToString().Substring(0, 4));
            }

        }




        public double StardateTNG
        {
            get
            {
                return this.ToStardateTNG();
            }
        }



        //[FullDateUK] CHAR(10), -- Date in dd-MM-yyyy format
        //[FullDateUSA] CHAR(10),-- Date in MM-dd-yyyy format
        //[DayOfMonth] VARCHAR(2), -- Field will hold day number of Month
        //[DaySuffix] VARCHAR(4), -- Apply suffix as 1st, 2nd ,3rd etc
        //[DayName] VARCHAR(9), -- Contains name of the day, Sunday, Monday 
        //[DayOfWeekUSA] CHAR(1),-- First Day Sunday=1 and Saturday=7
        //[DayOfWeekUK] CHAR(1),-- First Day Monday=1 and Sunday=7
        //[DayOfWeekInMonth] VARCHAR(2), --1st Monday or 2nd Monday in Month
        //[DayOfWeekInYear] VARCHAR(2),
        //[DayOfQuarter] VARCHAR(3),
        //[DayOfYear] VARCHAR(3),
        //[WeekOfMonth] VARCHAR(1),-- Week Number of Month 
        //[WeekOfQuarter] VARCHAR(2), --Week Number of the Quarter
        //[WeekOfYear] VARCHAR(2),--Week Number of the Year
        //[Month] VARCHAR(2), --Number of the Month 1 to 12
        //[MonthName] VARCHAR(9),--January, February etc
        //[MonthOfQuarter] VARCHAR(2),-- Month Number belongs to Quarter


        public string LunarPhase
        {
            get
            {
                return ((Enum.LunarPhaseSegments)this.ToLunarSegment()).ToString();
            }
        }

        public byte LunarPhaseSegment
        {
            get
            {
                return (byte)this.ToLunarSegment();
            }
        }

        public bool IsLeapYear
        {
            get
            {
                return DateTime.IsLeapYear(this.Year);
            }
        }

        public int ChineseLunarYear
        {
            get
            {
                try
                {
                    System.Globalization.ChineseLunisolarCalendar clc = new ChineseLunisolarCalendar();

                    return clc.GetYear(this);
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }



        public string ChineseZodiac
        {
            get
            {
                try
                {
                    var cc = new System.Globalization.ChineseLunisolarCalendar();
                    var SexanageryYear = cc.GetSexagenaryYear(this);
                    var TerrestrialBranch = cc.GetTerrestrialBranch(SexanageryYear);
                    //var cYear = "rat,ox,tiger,hare,dragon,snake,horse,sheep,monkey,fowl,dog,pig".Split(',');
                    var cYear = "Rat,Ox,Tiger,Rabbit,Dragon,Snake,Horse,Goat,Monkey,Rooster,Dog,Pig".Split(',');
                    return ((Enum.ChineseZodiac)(TerrestrialBranch - 1)).ToString();
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
        }

        #endregion Public Properties

        #region implicit operators

        public static implicit operator DateTime(JB2Date rhs)
        {

            //string strDate = rhs._datekey.ToString();
            //System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.DateTimeFormatInfo();
            //dtfi.ShortDatePattern = "yyyymmdd";
            //dtfi.DateSeparator = "";
            //DateTime objDate = Convert.ToDateTime(strDate, dtfi);

            if (rhs == null)
                return JB2Date.MinDate();

            if (rhs._dt != DateTime.MinValue)
                rhs._dt = new DateTime(rhs.Year, rhs.Month, rhs.DayOfMonth);
            return rhs._dt.GetValueOrDefault(DateTime.MinValue);
        }

        public static implicit operator JB2Date(DateTime jb2d)
        {
            string datekey = jb2d.ToString("yyyyMMdd");
            return new JB2Date(Convert.ToInt32(datekey));

        }

        public static implicit operator int (JB2Date rhs)
        {
            return rhs._datekey;
        }

        public static implicit operator JB2Date(int rhs)
        {
            return new JB2Date(rhs);
        }

        #endregion

        #region Static Method

        public static JB2Date MinDate()
        {
            return new JB2Date(MINKEY);
        }

        public static JB2Date MaxDate()
        {
            return new JB2Date(MAXKEY);
        }

        public bool IsValidKey(int key)
        {
            bool result = true;
            if (key < MINKEY || key > MAXKEY)
                return false;
            try
            {
                string strDate = key.ToString();
                System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.DateTimeFormatInfo();
                dtfi.ShortDatePattern = "yyyyMMdd";
                //dtfi.DateSeparator = "";
                DateTime objDate = DateTime.ParseExact(
                strDate,
                "yyyyMMdd",
                CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                return false;
            }

            return result;
        }

        #endregion Static Method


        #region To Convert Methods

        public DateTime ToDatetime()
        {
            return (DateTime)this;
        }

        public int ToInt()
        {
            return (int)this;
        }


        public int ToLunarSegment()
        {

            //source http://www.voidware.com/moon_phase.htm
            int c;
            int e;
            int b;
            double jd;

            int year = this.Year;
            int month = this.Month;
            int day = this.DayOfMonth;

            if (month < 3)
            {
                year--;
                month += 12;
            }
            ++month;
            c = (int)(365.25 * year);
            e = (int)(30.6 * month);
            jd = c + e + day - 694039.09;
            jd /= 29.53;
            b = (int)jd;
            jd -= b;
            b = (int)(jd * 8 + 0.5);
            b = b & 7;
            return b;

        }


        public double ToStardateTNG()
        {
            try
            {


                DateTime StardateOrigin = new DateTime(1987, 07, 15);
                DateTime TestDate = (DateTime)this;

                //source http://trekguide.com/Stardates.htm

                TimeSpan timespan = TestDate - StardateOrigin;

                var msec = timespan.TotalMilliseconds / (1000 * 60 * 60 * 24 * 0.036525);

                msec = Math.Floor(msec + 410000);
                msec = msec / 10;
                return msec;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public override string ToString()
        {
            var datetime = (DateTime)this;

            return datetime.ToString();
        }

        #endregion

    }
}
