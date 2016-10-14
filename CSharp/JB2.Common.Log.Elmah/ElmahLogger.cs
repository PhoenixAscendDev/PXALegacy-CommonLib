using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Elmah;

namespace JB2.Common.Log
{
    public class ElmahLogger : Logger,ILogger
    {

        #region Constructor
        public ElmahLogger(ILogRepo repo)
        {
            base.SetAllServerity(true);

        }
        #endregion Constructor


        #region ILogger
        public override void Log(ILogEntry entry)
        {
            try
            {
                if (IsEnabled(entry.Serverity))
                {
                    if (entry.Message != null)
                    {
                        var annotatedException = new Exception(entry.Message, entry.Exception);
                        ErrorSignal.FromCurrentContext().Raise(annotatedException);

                    }
                    else
                    {
                        ErrorSignal.FromCurrentContext().Raise(entry.Exception);
                    }
                }
                else
                {
                    string message = string.Format("The severity level of {0} is not enabled in the ElmahLogger", entry.Serverity.ToString());
                    throw new LogServerityNotEnabled(message);
                }
            }
            catch(Exception)
            {
                //not good not good
            }
        }

        #endregion ILogger
    }
}
