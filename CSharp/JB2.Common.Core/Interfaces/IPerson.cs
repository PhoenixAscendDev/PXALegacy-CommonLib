using System;
using System.Collections.Generic;

using System.Text;

namespace JB2.Common
{
    public interface IPerson<TKey> : IIDNamePair<TKey,string>
        where TKey : IComparable
    {      
        string DisplayName { get; set; }
        Name NameInfo { get; set; }      
    }
}
