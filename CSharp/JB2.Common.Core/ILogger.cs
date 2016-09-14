using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Enum;

namespace JB2.Common
{

    public interface ILogger : ILogger<LogServerityType,string,ILogEntry>
    {

    }


    public interface ILogger<TServerity,TKey,TLogEntry>
        where TServerity : IComparable
        where TLogEntry : ILogEntry<TKey,TServerity>
    {
        bool IsEnabled(TServerity severity);
        void Log(TLogEntry entry);       
    }
}
