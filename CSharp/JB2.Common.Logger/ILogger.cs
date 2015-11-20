using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Log.Enum;

namespace JB2.Common.Log
{

    public interface ILogger : ILogger<LogServerityType>
    {

    }


    public interface ILogger<TServerity>
        where TServerity : IComparable
    {
        bool IsDebugEnabled { get; }
        bool IsVerboseEnabled { get; }
        bool IsInformationalEnabled { get; }
        bool IsWarningEnabled { get; }
        bool IsFatalEnabled { get; }
        bool IsErrorEnabled { get; }

        void Write(string message, TServerity severity);
        void Write(string message, Exception exception, TServerity severity);
    }
}
