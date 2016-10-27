using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public interface ILogEntry : ILogEntry<string,Enum.LogServerityType>
    {

    }
    public interface ILogEntry<TKey, TSeverity> : ITagable<Tag>
    {
        TSeverity Serverity { get; }
        TKey ID { get; }
        string Message { get; }
        Exception Exception { get; }
        DateTime LogDate { get; }

        string LogCode { get; }
    }

    
}
