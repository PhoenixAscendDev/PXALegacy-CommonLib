using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public class class SettingNotFound : JB2Exception
    {
        public SettingNotFound()
            : this("Setting Not Found")
        {

        }

        public SettingNotFound(string message) : this(message, null)
        {

        }

        public SettingNotFound(string message, Exception innerException) : base(message, innerException, "E-01-100011")
        {

        }
    }
}
