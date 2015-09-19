using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class Validation : IValidation
    {
        internal Exception _expection;
        public string Name { get; set; }
        public string Message { get; set; }
        public bool IsValid { get; set; }
        public Validation()
        {
        }
        public Validation(Exception exception)
        {
            _expection = exception;
            this.Name = exception.GetType().ToString();
            this.Message = exception.Message;
        }

        public Validation(string name, string message)
        {
            this.Name = name;
            this.Message = message;
        }

        public Exception ToException()
        {

            if (_expection == null)
                return new Exceptions.ResultException(this.Message);
            else
                return this._expection;
        }

    }
}
