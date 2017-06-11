using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateImport
{
    public class JB2DateEntry : Microsoft.WindowsAzure.Storage.Table.TableEntity 
    {
       

        public JB2DateEntry(int datekey)
        {
            
        }
        public JB2DateEntry(string partitionKey, string rowKey, JB2.Common.JB2Date date)
            
        {

            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;

            this.DateKey = date.DateKey;
            this.DateUSA = date.DateUSA;
            this.ChineseLunarYear = date.ChineseLunarYear;
            this.ChineseZodiac = date.ChineseZodiac;
            this.DayOfMonth = date.DayOfMonth;
            this.DayofWeek = date.DayofWeek;
            this.DayofWeekName = date.DayofWeekName;
            this.DayofYear = date.DayofYear;
            this.DaySuffix = date.DaySuffix;
            this.IsLeapYear = date.IsLeapYear;
            this.IsWeekday = date.IsWeekday;
            this.IsWeekend = date.IsWeekend;
            this.LongName = date.LongName;
            this.LunarPhase = date.LunarPhase;
            this.LunarPhaseSegment = date.LunarPhaseSegment;
            this.Month = date.Month;
            this.MonthName = date.MonthName;
            this.MonthOfQuarter = date.MonthOfQuarter;
            this.StardateTNG = date.StardateTNG;
            this.Ticks = date.Ticks;
            this.WeekOfMonth = date.WeekOfMonth;
            this.WeekOfYear = date.WeekOfYear;
            this.Year = date.Year;
        }



        public JB2DateEntry()
        {
           
        }


        public int DateKey { get; set; }
        public string DateUSA { get; set; }
        public int ChineseLunarYear { get; set; }
        public string ChineseZodiac { get; set; }
        public int DayOfMonth { get; set; }
        public int DayofWeek { get; set; }
        public int DayofYear { get; set; }
        public string DayofWeekName { get; set; }
        public string DaySuffix { get; set; }
        public bool IsLeapYear { get; set; }
        public bool IsWeekday { get; set; }
        public bool IsWeekend { get; set; }
        public string LongName { get; set; }
        public string LunarPhase { get; set; }
        public int LunarPhaseSegment { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public int MonthOfQuarter { get; set; }
        public double StardateTNG { get; set; }
        public long Ticks { get; set; }
        public int Year { get; set; }
        public int WeekOfMonth { get; set; }
        public int WeekOfYear { get; set; }

        public bool IsDay { get; set; }
        public bool IsNight { get; set; }
        


    }
}
