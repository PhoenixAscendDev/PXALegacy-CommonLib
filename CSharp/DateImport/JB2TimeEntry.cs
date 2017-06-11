using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateImport
{
    public class JB2TimeEntry : Microsoft.WindowsAzure.Storage.Table.TableEntity
    {


        public JB2TimeEntry(int timekey)
        {

        }
        public JB2TimeEntry(string partitionKey, string rowKey, JB2.Common.JB2Time time)
        {


            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;

            AMPM = time.AmPm;
            DayNight = time.DayNight;
            DaytimeName = time.DaytimeName;
            HalfHour = time.HalfHour;
            HalfHourOfDay = time.HalfHourOfDay;
            Hour = time.Hour;
            HourOfDay12 = time.HourOfDay12;
            HourOfDay24 = time.HourOfDay24;
            Minute = time.Minute;
            MinuteOfDay = time.MinuteOfDay;
            MinuteOfHour = time.MinuteOfHour;
            QuarterHour = time.QuarterHour;
            QuarterHourOfDay = time.QuarterHourOfDay;
            Second = time.Second;
            SecondOfDay = time.SecondOfDay;
            SecondOfHour = time.SecondOfHour;
            SecondOfMinute = time.SecondOfMinute;
            TimeAltKey = time.TimeAltKey;
            TimeKey = time.TimeKey;
            TimeMilitaryUSA = time.TimeMilitaryUSA;
            TimeMinUSA = time.TimeMinUSA;
            TimeMinUSA24 = time.TimeMinUSA24;
            TimeMinUSASansPeriod = time.TimeMinUSASansPeriod;
            TimeOfDay = time.TimeOfDay.ToString();
            TimeSecUSA = time.TimeSecUSA;
            TimeSecUSA24 = time.TimeSecUSA24;
            TimeSecUSASansPeriod = time.TimeSecUSASansPeriod;
            isDay = time.DayNight == "Day" ? true : false;
            isNight = time.DayNight == "Night" ? true : false;
            isAM = time.AmPm == "AM" ? true : false;
            isPM = time.AmPm == "PM" ? true : false;

            
            
        }


        #region Properties

        public string AMPM { get; set; }
        public string DayNight { get; set; }
        public string DaytimeName { get; set; }
        public int HalfHour { get; set; }
        public int HalfHourOfDay { get; set; }
        public int Hour { get; set; }
        public int HourOfDay12 { get; set; }
        public int HourOfDay24 { get; set; }
        public int Minute { get; set; }
        public int MinuteOfDay { get; set; }
        public int MinuteOfHour { get; set; }
        public int QuarterHour { get; set; }
        public int QuarterHourOfDay { get; set; }
        public int Second { get; set; }
        public int SecondOfDay { get; set; }
        public int SecondOfHour { get; set; }
        public int SecondOfMinute { get; set; }
        public int TimeKey { get; set; }
        public int TimeAltKey { get; set; }
        public string TimeMilitaryUSA { get; set; }
        public string TimeMinUSA { get; set; }
        public string TimeMinUSA24 { get; set; }
        public string TimeMinUSASansPeriod { get; set; }
        public string TimeOfDay { get; set; }
        public string TimeSecUSA { get; set; }
        public string TimeSecUSA24 { get; set; }
        public string TimeSecUSASansPeriod { get; set; }
        public bool isDay { get; set; }
        public bool isNight { get; set; }
        public bool isAM { get; set; }
        public bool isPM { get; set; }



        #endregion Properties



    }
}
