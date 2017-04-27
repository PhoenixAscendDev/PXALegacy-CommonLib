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
            logger.Log(LogEntry.NewLogEntry(serverity, exception));
        }

        public static void LogInformation(this ILogger logger, string message)
        {
            logger.Log(LogEntry.NewLogEntry(Enum.LogServerityType.Informational, message));
        }

        public static void LogError(this ILogger logger, Exception ex)
        {
            logger.Log(LogEntry.NewLogEntry(Enum.LogServerityType.Error, ex));
        }

        public static void LogDebug(this ILogger logger, string message)
        {
            logger.Log(LogEntry.NewLogEntry(Enum.LogServerityType.Debug, message));
        }
    }

    public static class LogRepoExtenstion
    {
        public static IEnumerable<ILogEntry> GetLogEntriesByServerity(this ILogRepo repo, Enum.LogServerityType serverity)
        {

            return (IEnumerable<ILogEntry>)repo.GetLogEntries(LogSearch.SearchBySeverity(serverity, QueryComparison.Equal));
        }

        public static IEnumerable<ILogEntry> GetLogEntriesByDate(this ILogRepo repo, DateTime date, string comparison)
        {
            return (IEnumerable<ILogEntry>)repo.GetLogEntry(LogSearch.SearchByDate(date, comparison));
        }


        public static ILogEntry GetLogEntryById(this ILogRepo repo, string id)
        {
            return (ILogEntry)repo.GetLogEntry(LogSearch.SearchByID(id, QueryComparison.Equal));
        }

        public static IEnumerable<ILogEntry> GetMostRecentLogEntries(this ILogRepo repo,int maxRecords)
        {
            return (IEnumerable<ILogEntry>)repo.GetLogEntries(new LogSearch(null, null, DateTime.MinValue, "eq", maxRecords));
        }

    }

    public static class ExceptionExtenstion
    {
        public static void LogIt(this Exception ex, ILogger logger, Enum.LogServerityType serverity)
        {
            logger.Log(serverity, ex);
        }

        public static void LogIt(this Exception ex, ILogger logger)
        {
            logger.LogError(ex);
        }
        // More methods here.
    }
}
