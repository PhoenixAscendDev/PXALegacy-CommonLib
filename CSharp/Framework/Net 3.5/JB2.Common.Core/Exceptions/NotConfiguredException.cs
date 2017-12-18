using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Common
{
    public class NotConfiguredException : JB2Exception
    {
        public NotConfiguredException()
            : this("Setting was not configured")
        {

        }

        public NotConfiguredException(string message) : this(message, null)
        {

        }

        public NotConfiguredException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
