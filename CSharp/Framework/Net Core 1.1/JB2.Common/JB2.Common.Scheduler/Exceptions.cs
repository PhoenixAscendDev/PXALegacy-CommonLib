using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Scheduler
{
    public class SchedulerException : JB2Exception
    {
        public SchedulerException()
            : this("Error during the execution of a Scheduler")
        {

        }

        public SchedulerException(string message) : this(message,null,null)
        {

        }

        public SchedulerException(string message, string errorCode) : this(message, null,null)
        {

        }

        public SchedulerException(string message,Exception innerException, string errorCode) : base(message,innerException,errorCode)
        {
           
        }
    }
}
