using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class DateTimeMetaData : MetaData<DateTime>, IMetaData
    {
        
        public DateTimeMetaData(string propertyName, DateTime value) : base(propertyName,value)
        {
            
        }
     
        public static implicit operator DateTime(DateTimeMetaData d)
        {
            return d._value;
        }
    }
}
