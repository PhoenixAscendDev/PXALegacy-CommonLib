using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public static class ExceptionExtenstions
    {
        public static JB2Exception ToJB2Exception(this Exception ex, string errorCode)
        {
            var result = new JB2Exception(ex.Message, ex, errorCode);
            result.ErrorCode = errorCode;

            return result;
        }
    }
}
