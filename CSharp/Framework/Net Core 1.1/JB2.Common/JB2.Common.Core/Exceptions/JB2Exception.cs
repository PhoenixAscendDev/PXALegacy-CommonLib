using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{

    public interface IException
    {
        string ErrorCode { get; set; }
    }
    
    public class JB2Exception : Exception, IException
    {

        #region Fields

        protected string _code;

        #endregion Fields
        public JB2Exception()
            : this("Setting was not configured")
        {

        }

        public JB2Exception(string message, string code="") : this(message, null,code)
        {

        }

        public JB2Exception(string message, Exception innerException, string code="") : base(message, innerException)
        {
            _code = code;
        }

        public string Code { get => _code; }
        public string ErrorCode { get => _code; set => _code = value; }

        

    }
}
