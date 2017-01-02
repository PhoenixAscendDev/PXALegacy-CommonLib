using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class ServiceResult : ServiceResult<object>
    {
        #region Constructor

        public ServiceResult(object obj) : base(obj)
        {

        }


        public ServiceResult() : base()
        {

        }

        public ServiceResult(Exception ex) : base(ex)
        {

        }

        #endregion Constructor

        public static implicit operator ServiceResult(bool b)
        {
            return (ServiceResult)b;
        }

        public static implicit operator bool(ServiceResult sr)
        {
            return (bool)sr;
        }

        public static implicit operator Exception(ServiceResult sr)
        {
            return (Exception)sr;
        }

        public static implicit operator List<IValidation>(ServiceResult sr)
        {
            return (List<IValidation>)sr;

        }

        public static implicit operator ServiceResult(List<IValidation> v)
        {
            return (ServiceResult)v;
        }

        public static implicit operator ServiceResult(string s)
        {
            return (ServiceResult)s;
        }


    }

    public class ServiceResult<Tobject> : IServiceResult<Tobject>
    {
        private List<IValidation> _validation;
        private Tobject _object;


        public ServiceResult(Tobject obj)
        {
            _object = obj;
        }


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

        public Tobject ToObject()
        {
            return _object;
        }

        public static implicit operator bool(ServiceResult<Tobject> sr)
        {
            return sr._validation == null || sr._validation.Count == 0 || !sr._validation.Exists(v => !v.IsValid);
        }

        public static implicit operator ServiceResult<Tobject>(bool b)
        {
            return new ServiceResult<Tobject>() { _validation = b ? null : new List<IValidation>(new IValidation[] { new Validation() }) };
        }

        public static implicit operator Exception(ServiceResult<Tobject> sr)
        {
            if (sr.Count > 0)
                return sr._validation[0].ToException();
            else
                return new ResultException("IsValid");
        }

        public static implicit operator Tobject(ServiceResult<Tobject> sr)
        {
            return sr.ToObject();
        }

        public static implicit operator List<IValidation>(ServiceResult<Tobject> sr)
        {
            return sr.Validation;

        }

        public static implicit operator ServiceResult<Tobject>(List<IValidation> v)
        {
            return new ServiceResult<Tobject>() { _validation = v, };
        }

        public static implicit operator ServiceResult<Tobject>(string s)
        {
            return new ServiceResult<Tobject>{ _validation = new List<IValidation>(new IValidation[] { new Validation() { Message = string.IsNullOrEmpty(s) ? null : s, } }) };
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
