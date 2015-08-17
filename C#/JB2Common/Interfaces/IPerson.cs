using System;
using System.Collections.Generic;

using System.Text;

namespace JB2.Common
{
    public interface IPerson<TKey> : IIDNamePair<TKey,string>
    {
        
        string DisplayName { get; set; }
        Name NameInfo { get; set; }
    }
}
