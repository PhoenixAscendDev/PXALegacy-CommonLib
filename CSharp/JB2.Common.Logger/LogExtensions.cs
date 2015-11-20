using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Log
{
    public static class LoggerExtensions
    {
        public static void Log(this ILogger logger, Enum.LogServerityType serverity, string message)
        {
            logger.Log(LogEntry.NewLogEntry(serverity, message));
        }

        public static void Log(this ILogger logger, Enum.LogServerityType serverity, Exception exception)
        {
            logger.Log(LogEntry.NewLogEntry(serverity,exception));
        }

        public static void LogIt(this Exception ex, ILogger logger,Enum.LogServerityType serverity)
        {
            logger.Log(serverity, ex);
        }

        public static void LogIt(this Exception ex, ILogger logger)
        {
            logger.Log(Enum.LogServerityType.Error, ex);
        }
        // More methods here.
    }
}
