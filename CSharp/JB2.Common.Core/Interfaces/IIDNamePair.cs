using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface IIDNamePair<TKey,TName> : IIDable<TKey>,INameable<TName>
        where TKey : IComparable
        where TName : IComparable
    {
        TKey ID { get; set; }
        TName Name { get; set; }
    }
}
