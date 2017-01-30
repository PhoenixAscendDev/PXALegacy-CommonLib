using System;
using System.Collections.Generic;
using System.Text;


namespace JB2.Common.Log
{
    public interface ILogRepo :  ILogRepo<string,Enum.LogServerityType,ILogEntry,string,ILogSearch>
    {

    }
    public interface ILogRepo<TKey,TServerity,TLogEntry,TComparsion,TSearch>
      where TLogEntry : ILogEntry<TKey,TServerity>
      where TComparsion : IComparable
      where TSearch : ILogSearch<TKey,TServerity,TComparsion>
    {
        IEnumerable<TLogEntry> GetLogEntries(TSearch search);
        TLogEntry GetLogEntry(TSearch search);

        bool StoreLogEntry(TLogEntry entry);
    }

   
}
