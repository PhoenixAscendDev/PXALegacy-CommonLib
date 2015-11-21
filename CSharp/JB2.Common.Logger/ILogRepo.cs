using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Log
{
    public interface ILogRepo :  ILogRepo<string,Enum.LogServerityType,ILogEntry,string>
    {

    }
    public interface ILogRepo<TKey,TServerity,TLogEntry,TComparsion>
      where TLogEntry : ILogEntry<TKey,TServerity>
      where TComparsion : IComparable
    {
        IEnumerable<TLogEntry> GetLogEntries(ILogSearch<TKey,TServerity,TComparsion> search);
        TLogEntry GetLogEntry(ILogSearch<TKey, TServerity, TComparsion> search);
    }
}
