using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class JB2Exception : Exception
    {
        public JB2Exception()
            : this("Setting was not configured")
        {

        }

        public JB2Exception(string message, string code="") : this(message, null,code)
        {

        }

        public JB2Exception(string message, Exception innerException, string code="") : base(message, innerException)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
