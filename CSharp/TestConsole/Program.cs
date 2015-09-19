using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime StardateOrigin = new DateTime(1987,07,15);
            DateTime TestDate = new DateTime(1980, 02, 22);

            //TimeSpan TestTimeSpan = TestDate - new DateTime(1970,01,01);
            //TimeSpan OriginTimeSpan = StardateOrigin - new DateTime(1970, 01, 01);

            TimeSpan timespan = TestDate - StardateOrigin;

            //TimeSpan timespan = TestTimeSpan - OriginTimeSpan;
            var msec = timespan.TotalMilliseconds / (1000 * 60 * 60 * 24 * 0.036525);

            msec = Math.Floor(msec + 410000);
            msec = msec / 10;
           // Console.Write(TestTimeSpan.TotalMilliseconds);
           // Console.WriteLine();
           // Console.Write(OriginTimeSpan.TotalMilliseconds);
           // Console.WriteLine();
           // Console.Write(timespan.TotalMilliseconds);
           // Console.WriteLine();
            Console.Write(msec);
            Console.ReadLine();
//            var StardateOriginToday = new Date("July 15, 1987 00:00:00");
//var StardateInputToday = new Date();

//StardateInputToday.setYear(YearInput)
//StardateInputToday.setMonth(MonthInput)
//StardateInputToday.setDate(DayInput)
//StardateInputToday.setHours(HourInput)
//StardateInputToday.setMinutes(MinuteInput)
//StardateInputToday.setSeconds(0)
//StardateInputToday.toGMTString(0)

//var stardateToday = StardateInputToday.getTime() - StardateOriginToday.getTime();
//stardateToday = stardateToday / (1000 * 60 * 60 * 24 * 0.036525);
//stardateToday = Math.floor(stardateToday + 410000);
//stardateToday = stardateToday / 10
        }
    }
}
