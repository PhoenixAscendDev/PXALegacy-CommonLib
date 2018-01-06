using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Common.Scheduler
{
    public class SchedulerException : Exception
    {
        public SchedulerException()
            : this("Error during the execution of a Scheduler")
        {

        }

        public SchedulerException(string message) : this(message, null)
        {

        }

        public SchedulerException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
