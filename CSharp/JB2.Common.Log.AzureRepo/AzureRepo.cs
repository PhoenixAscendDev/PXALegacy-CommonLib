using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common.Log.Enum;

namespace JB2.Common.Log
{
    public class AzureRepo : ILogRepo
    {
        public IEnumerable<ILogEntry> GetLogEntries(ILogSearch<string, LogServerityType, string> search)
        {
            throw new NotImplementedException();
        }

        public ILogEntry GetLogEntry(ILogSearch<string, LogServerityType, string> search)
        {
            throw new NotImplementedException();
        }
    }
}
