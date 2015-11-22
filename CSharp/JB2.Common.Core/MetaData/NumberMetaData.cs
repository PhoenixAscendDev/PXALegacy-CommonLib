using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class NumberMetaData : MetaData<int>, IMetaData
    {
        

        public NumberMetaData(string propertyName, int value) : base(propertyName,value)
        {

        }
        

        public static implicit operator int(NumberMetaData d)
        {
            return d._value;
        }
    }
}
