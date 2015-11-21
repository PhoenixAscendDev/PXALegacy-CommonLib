using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common.Log.Enum;

namespace JB2.Common.Log.Elmah
{
    public class ElmahLogger : Logger,ILogger
    {


        public override void Log(ILogEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}
