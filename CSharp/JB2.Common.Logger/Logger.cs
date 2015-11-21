using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common.Log.Enum;

namespace JB2.Common.Log
{
    public abstract class Logger : ILogger
    {
        private Dictionary<Enum.LogServerityType, bool> _serverityFlag = new Dictionary<LogServerityType, bool>();

        public virtual bool IsEnabled(LogServerityType serverity)
        {
            if (!_serverityFlag.ContainsKey(serverity))
                return false;
            else
                return _serverityFlag[serverity];
        }

        public void SetServerityType(Enum.LogServerityType type, bool enable)
        {
            if (!_serverityFlag.ContainsKey(type))
                _serverityFlag.Add(type, enable);
            else
                _serverityFlag[type] = enable;
        }

        public abstract void Log(ILogEntry entry);
    }
}
