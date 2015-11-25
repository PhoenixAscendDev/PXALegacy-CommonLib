using System;
using System.Collections.Generic;

using System.Text;

namespace JB2.Common
{
    public interface IPerson<TKey> : IIDNamePair<TKey,Name>
        where TKey : IComparable
    {      
        string DisplayName { get; set; }
        
    }
}
