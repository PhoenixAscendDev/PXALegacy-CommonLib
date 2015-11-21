using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Log
{

    public interface ILogSearch : ILogSearch<string,Enum.LogServerityType,string>
    {

    }
    public interface ILogSearch<TKey,TServerity,TComparsion>
    {
        IEnumerable<TKey> IDs { get; set; }
        IEnumerable<TServerity> Serveritys { get; set; }
        DateTime LogDate { get; set; }

        int MaxRecordReturned { get; set; }
        TComparsion Comparison { get; set; }
    }
}
