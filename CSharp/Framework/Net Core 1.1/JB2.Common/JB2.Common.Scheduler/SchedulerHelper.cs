using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace JB2.Helpers
{
    public class SchedulerHelper
    {
        public static bool IsRealJob(Type test)
        {
            return test.GetTypeInfo().IsAbstract == false
                && test.GetTypeInfo().IsGenericTypeDefinition == false
                && test.GetTypeInfo().IsInterface == false;
        }
    }
}
