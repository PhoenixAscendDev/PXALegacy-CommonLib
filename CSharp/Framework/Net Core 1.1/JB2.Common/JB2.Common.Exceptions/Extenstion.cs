using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public static class ExceptionExtenstions
    {
        public static JB2Exception ToJB2Exception(this Exception ex, string errorCode="")
        {
            if (String.IsNullOrEmpty(errorCode))
                errorCode = "E-00-" + JB2.Common.Hash.HashString(ex.GetType().FullName, Common.Enum.HashType.Adler32);


            //add to dictionary
            JB2.Dictionary.ErrorCodes.Add(new StatusCode(errorCode, ex.Message));

            var result = new JB2Exception(ex.Message, ex, errorCode);
            result.ErrorCode = errorCode;

            return result;
        }
    }
}
