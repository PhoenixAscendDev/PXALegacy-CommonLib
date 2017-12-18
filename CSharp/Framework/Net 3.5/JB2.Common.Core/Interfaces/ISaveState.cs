using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Common
{
    public interface ISaveState<TObjectType,TObjectKey>
    {
        TObjectKey  ObjectID { get; set;}
        DateTime DateSaved { get; set; }
        TObjectType ObjectType { get; set; }
        MetaDataCollection Properties { get; set; }
        string ToJSON();
    }
}
