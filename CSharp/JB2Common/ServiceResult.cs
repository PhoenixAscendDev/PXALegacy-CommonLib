using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class ServiceResult : IServiceResult
    {
        private List<IValidation> _validation;

        public ServiceResult()
        {

        }

        public ServiceResult(Exception ex)
        {
            _validation = new List<IValidation>();
            _validation.Add(new Validation(ex.ToString(), ex.Message) { IsValid = false });
        }

        public int Count
        {
            get
            {
                return this._validation == null ? 0 : this._validation.Count;
            }
        }

        public List<IValidation> Validation
        {
            get
            {
                if (this._validation == null)
                {
                    this._validation = new List<IValidation>();
                }
                return this._validation;
            }

            set
            {
                this._validation = value;

            }
        }

        public static implicit operator bool(ServiceResult sr)
        {
            return sr._validation == null || sr._validation.Count == 0 || !sr._validation.Exists(v => !v.IsValid);
        }

        public static implicit operator ServiceResult(bool b)
        {
            return new ServiceResult() { _validation = b ? null : new List<IValidation>(new IValidation[] { new Validation() }) };
        }

        public static implicit operator Exception(ServiceResult sr)
        {
            if (sr.Count > 0)
                return sr._validation[0].ToException();
            else
                return new Exceptions.ResultException("IsValid");
        }

        public static implicit operator List<IValidation>(ServiceResult sr)
        {
            return sr.Validation;

        }

        public static implicit operator ServiceResult(List<IValidation> v)
        {
            return new ServiceResult() { _validation = v, };
        }

        public static implicit operator ServiceResult(string s)
        {
            return new ServiceResult { _validation = new List<IValidation>(new IValidation[] { new Validation() { Message = string.IsNullOrEmpty(s) ? null : s, } }) };
        }

        public IValidation[] ToArray()
        {
            return this._validation == null ? new Validation[] { } : this._validation.ToArray();
        }

        public override string ToString()
        {
            IValidation firstError = this._validation == null ? null : this._validation.Find(v => !v.IsValid && !string.IsNullOrEmpty(v.Message));
            return this._validation == null || firstError == null ? null : firstError.Message;
        }

        public bool ToBool()
        {
            return (bool)this;
        }
    }
}
