using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{



    public class MetaDataValue : IMetaDataValue
    {
        #region Field
        object _value;
        #endregion Field

        #region Static Implicit

        public static implicit operator bool(MetaDataValue v)
        {
            return v.BooleanValue;
        }

        public static implicit operator int(MetaDataValue v)
        {
            return v.IntValue;
        }

        public static implicit operator long(MetaDataValue v)
        {
            return v.LongValue;
        }

        public static implicit operator short(MetaDataValue v)
        {
            return v.ShortValue;
        }

        public static implicit operator string(MetaDataValue v)
        {
            return v.StringValue;
        }

        public static implicit operator DateTime(MetaDataValue v)
        {
            return v.DateTimeValue;
        }

        public static implicit operator double(MetaDataValue v)
        {
            return v.DoubleValue;
        }

        public static implicit operator Guid(MetaDataValue v)
        {
            return v.GuidValue;
        }

        #endregion Static Implicit


        #region Value Properties

        public MetaDataValue(object value)
        {
            _value = value;
        }

        public bool BooleanValue
        {
            get
            {
                return Convert.ToBoolean(_value);
            }
        }

        public DateTime DateTimeValue
        {
            get
            {
                return Convert.ToDateTime(_value);
            }
        }

        public double DoubleValue
        {
            get
            {
                return Convert.ToDouble(_value);
            }
        }

        public Guid GuidValue
        {
            get
            {
                return (Guid)_value;
            }
        }

        public int IntValue
        {
            get
            {
                return Convert.ToInt32(_value);
            }
        }

        public long LongValue
        {
            get
            {
                return Convert.ToInt64(_value);
            }
        }

        public object ObjectValue
        {
            get
            {
                return _value;
            }
        }

        public short ShortValue
        {
            get
            {
                return Convert.ToInt16(_value);
            }
        }

        public string StringValue
        {
            get
            {
                return Convert.ToString(_value);
            }
        }

        #endregion Value Properties
    }
}
