using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public class SettingsNotConfiguredException : JB2Exception
    {
        public SettingsNotConfiguredException()
            : this("Settings was not configured")
        {

        }

        public SettingsNotConfiguredException(string message) : this(message, null)
        {

        }

        public SettingsNotConfiguredException(string message, Exception innerException) : base(message, innerException, "E-01-100010")
        {

        }
    }
}
