using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common.Error
{
    public class SqliteException : Exception
    {
        public SqliteException(string message)
            : base(message)
        {

        }
    }
}
