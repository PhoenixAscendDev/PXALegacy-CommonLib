using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IMetaData
    {
        string PropertyName { get; }
        Type PropertyType { get;}

        object GetValue();
      //  object Value { get; set; }
    }

    //public interface IMetaData<T> : IMetaData
    //{
    //    string PropertyName { get; set; }
    //    Type PropertyType { get; set; }
    //    //object Value { get; set; }

    //}
}
