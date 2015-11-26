using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface IIDNamePair<TKey,TName> : IIDProp<TKey>,INameProp<TName>
        where TKey : IComparable
        where TName : IComparable
    {
        TKey ID { get; set; }
        TName Name { get; set; }
    }
}
