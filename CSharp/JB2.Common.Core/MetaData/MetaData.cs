using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class MetaData<T> : IMetaData
    {
        protected  T _value;
        protected string _propertyName;

        public MetaData(string propertyName, T value)
        {
            _value = value;
            _propertyName = propertyName;
        }

        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
        }

        public Type PropertyType
        {
            get
            {
                return _value.GetType();
            }
        }

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public object GetValue()
        {
            return _value;
        }

        public static  implicit operator T(MetaData<T> d)
        {
            return d._value;
        }

        
    }
}
