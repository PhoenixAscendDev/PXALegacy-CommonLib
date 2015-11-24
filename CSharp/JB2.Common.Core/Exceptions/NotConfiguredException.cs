using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Exceptions
{
    public class NotConfiguredException : Exception
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
