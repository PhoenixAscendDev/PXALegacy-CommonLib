using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class ResultException : System.Exception
    {
        private string stackTraceOverride;

        public ResultException(string message)
            : base(message)
        {
        }

        public ResultException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public void SetStackTrace(string stackTrace)
        {
            var lines = new List<string>(stackTrace.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
            while (lines.Count > 0 && lines[0].IndexOf(".LogMessage(") > 0)
            {
                lines.RemoveAt(0);
            }

            this.stackTraceOverride = String.Join("\r\n", lines.ToArray());
        }

        public override string StackTrace
        {
            get
            {
                return this.stackTraceOverride ?? base.StackTrace;
            }
        }
    }
}
