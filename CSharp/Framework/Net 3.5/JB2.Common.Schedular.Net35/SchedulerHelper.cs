using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Helpers
{
    public class SchedulerHelper
    {
        public static bool IsRealJob(Type test)
        {
            return test.IsAbstract == false
                && test.IsGenericTypeDefinition == false
                && test.IsInterface == false;
        }
    }
}
