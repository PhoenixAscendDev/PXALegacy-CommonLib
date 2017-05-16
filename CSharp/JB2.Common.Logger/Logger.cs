using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using JB2.Common;
using JB2.Common.Enum;

namespace JB2.Common.Log
{
    public abstract class Logger : ILoggerAsync
    {

        #region Fields
        private Dictionary<Enum.LogServerityType, bool> _serverityFlag = new Dictionary<LogServerityType, bool>();

        #endregion Fields

        #region Constructors

        public Logger()
        {

        }

        #endregion Constructors



        #region events
        public event Action<ILogger<LogServerityType, string, ILogEntry>, LogServerityType, ILogEntry> EntryLogged;
        public event Action<ILogger<LogServerityType, string, ILogEntry>, ILogEntry> InfomationalLogged;
        public event Action<ILogger<LogServerityType, string, ILogEntry>, ILogEntry> WarningLogged;
        public event Action<ILogger<LogServerityType, string, ILogEntry>, ILogEntry> ErrorLogged;
        public event Action<ILogger<LogServerityType, string, ILogEntry>, ILogEntry> FatelLogged;
        public event Action<ILogger<LogServerityType, string, ILogEntry>, ILogEntry> DebugLogged;
        public event Action<ILogger<LogServerityType, string, ILogEntry>, ILogEntry> VerboseLogged;

        public void OnEntryLogged(ILogger<LogServerityType, string, ILogEntry> logger, LogServerityType serverity, ILogEntry logEntry)
        {
            if (EntryLogged != null)
                EntryLogged(logger, serverity, logEntry);
        }

        public void OnInformationalLogged(ILogger<LogServerityType, string, ILogEntry> logger, ILogEntry logEntry)
        {
            if (InfomationalLogged != null)
                InfomationalLogged(logger, logEntry);
        }

        public void OnWarningLogged(ILogger<LogServerityType, string, ILogEntry> logger, ILogEntry logEntry)
        {
            if (WarningLogged != null)
                WarningLogged(logger, logEntry);
        }
        public void OnErrorLogged(ILogger<LogServerityType, string, ILogEntry> logger, ILogEntry logEntry)
        {
            if (ErrorLogged != null)
                ErrorLogged(logger, logEntry);
        }
        public void OnFatelLogged(ILogger<LogServerityType, string, ILogEntry> logger, ILogEntry logEntry)
        {
            if (FatelLogged != null)
                FatelLogged(logger, logEntry);
        }
        public void OnDebugLogged(ILogger<LogServerityType, string, ILogEntry> logger, ILogEntry logEntry)
        {
            if (DebugLogged != null)
                DebugLogged(logger, logEntry);
        }
        public void OnVerboseLogged(ILogger<LogServerityType, string, ILogEntry> logger, ILogEntry logEntry)
        {
            if (VerboseLogged != null)
                VerboseLogged(logger, logEntry);
        }


        #endregion events



        public abstract JB2.Common.Log.Logit GetLogMethod();

        public virtual bool IsEnabled(LogServerityType serverity)
        {
            if (!_serverityFlag.ContainsKey(serverity))
                return false;
            else
                return _serverityFlag[serverity];
        }

        public void SetAllServerity(bool enable)
        {
            _serverityFlag = new Dictionary<LogServerityType, bool>();
            foreach (Enum.LogServerityType e in System.Enum.GetValues(typeof(Enum.LogServerityType)))
            {
                _serverityFlag.Add(e, enable);
            }
        }

        public void SetServerityType(Enum.LogServerityType type, bool enable)
        {
            if (!_serverityFlag.ContainsKey(type))
                _serverityFlag.Add(type, enable);
            else
                _serverityFlag[type] = enable;
        }

        public virtual async Task LogAsync(ILogEntry entry)
        {
            if (IsEnabled(entry.Serverity))
            {
                var workResult = await Task.Run(() => GetLogMethod()?.Invoke(entry));

                if (workResult)
                {
                    if (this.EntryLogged != null)
                        EntryLogged(this, entry.Serverity, entry);
                    switch (entry.Serverity)
                    {
                        case LogServerityType.Debug:
                            if (this.DebugLogged != null)
                                DebugLogged(this, entry);
                            break;
                        case LogServerityType.Error:
                            if (this.ErrorLogged != null)
                                ErrorLogged(this, entry);
                            break;
                        case LogServerityType.Fatel:
                            if (this.FatelLogged != null)
                                FatelLogged(this, entry);
                            break;
                        case LogServerityType.Informational:
                            if (this.InfomationalLogged != null)
                                InfomationalLogged(this, entry);
                            break;
                        case LogServerityType.Verbose:
                            if (this.VerboseLogged != null)
                                VerboseLogged(this, entry);
                            break;
                        case LogServerityType.Warning:
                            if (this.WarningLogged != null)
                                WarningLogged(this, entry);
                            break;
                    }
                }
            }
        }

        public virtual void Log(ILogEntry entry)
        {
            LogAsync(entry);
        }

        public void LogDebugMessage(string message, string logcode = "")
        {
            LogDebugMessageAsync(message, logcode);
        }

        public void LogError(Exception ex, string message = "", string logcode = "")
        {
            LogErrorAsync(ex, message, logcode);
        }

        public void LogMessage(string message, string logcode = "")
        {
            LogMessageAsync(message, logcode);
        }

        public abstract Task LogDebugMessageAsync(string message, string logcode = "");

        public abstract Task LogErrorAsync(Exception ex, string message = "", string logcode = "");

        public abstract Task LogMessageAsync(string message, string logcode = "");

    }
}
