using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class StringMetaData : MetaData<string>, IMetaData
    {
        
        public StringMetaData(string propertyName, string value) : base(propertyName,value)
        {
                        
        }


        public static implicit operator string(StringMetaData d)
        {
            return d._value;
        }
    }
}
