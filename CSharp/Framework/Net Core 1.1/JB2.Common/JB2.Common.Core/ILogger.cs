using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Enum;

namespace JB2.Common
{

    public interface ILogger<TServerity, TKey, TLogEntry>
        where TServerity : IComparable
        where TLogEntry : ILogEntry<TKey, TServerity>
    {
        bool IsEnabled(TServerity severity);
        void Log(TLogEntry entry);

        void LogDebugMessage(string message, string logcode = "");

        void LogError(Exception ex, string message = "", string logcode = "");

        void LogMessage(string message, string logcode = "");

        #region Events
        event Action<ILogger<TServerity, TKey, TLogEntry>, TServerity, TLogEntry> EntryLogged;

        void OnEntryLogged(ILogger<TServerity, TKey, TLogEntry> logger, TServerity serverity, TLogEntry logentry);

        #endregion Events
    }


    public interface ILoggerAsync : ILoggerAsync<LogServerityType, string, ILogEntry>
    {

    }

    public interface ILoggerAsync<TServerity, TKey, TLogEntry> : ILogger<TServerity, TKey, TLogEntry>
        where TServerity : IComparable
        where TLogEntry : ILogEntry<TKey, TServerity>
    {
        Task LogAsync(TLogEntry entry);

        Task LogDebugMessageAsync(string message, string logcode = "");

        Task LogErrorAsync(Exception ex, string message = "", string logcode = "");

        Task LogMessageAsync(string message, string logcode = "");
    }

}
