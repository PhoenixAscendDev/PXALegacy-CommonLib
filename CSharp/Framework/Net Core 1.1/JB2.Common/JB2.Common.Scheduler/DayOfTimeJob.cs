using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Scheduler
{
    public abstract class DayOfWeekTimeJob : RepeatableJob
    {
        public abstract override ServiceResult DoWork();
       

        public override int GetCoolDownSeconds()
        {
            return 300;  //5 Minutes;
        }

        public override int GetMaxCounter()
        {
            return int.MaxValue;
        }

        public override async Task DoWorkAndSetFlags()
        {
            var today = DateTime.Now;
            var range = GetDayofWeekTimeRange();

            //if within range then do the work;
            if(range.IsWithinRange(today))
                await base.DoWorkAndSetFlags();

        }



        public abstract DayofWeekTimeRange GetDayofWeekTimeRange();
    }
}
